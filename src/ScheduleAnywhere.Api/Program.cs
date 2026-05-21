using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Scalar.AspNetCore;
using Serilog;
using Serilog.Events;
using ScheduleAnywhere.Api.Middleware;
using ScheduleAnywhere.Api.Services;
using ScheduleAnywhere.Core.Interfaces;
using ScheduleAnywhere.Infrastructure.Data;
using ScheduleAnywhere.Infrastructure.Repositories;
using ScheduleAnywhere.Services.Implementations;
using ScheduleAnywhere.Services.Interfaces;
using System.Text;

// ── Serilog bootstrap ─────────────────────────────────────────────────────────
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("System", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext}: {Message:lj}{NewLine}{Exception}")
    .WriteTo.File("logs/app-.log", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 14)
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, services, cfg) => cfg
        .ReadFrom.Configuration(ctx.Configuration)
        .ReadFrom.Services(services)
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
        .Enrich.FromLogContext()
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext}: {Message:lj}{NewLine}{Exception}")
        .WriteTo.File("logs/app-.log", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 14));

    // ── Database ──────────────────────────────────────────────────────────────
    var connectionString = builder.Configuration.GetConnectionString("Default")
        ?? throw new InvalidOperationException("Connection string 'Default' is missing.");

    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
            mysql => mysql.SchemaBehavior(MySqlSchemaBehavior.Ignore)));

    // ── JWT Authentication ────────────────────────────────────────────────────
    var jwtSection = builder.Configuration.GetSection("Jwt");
    var jwtKey = jwtSection["Key"] ?? throw new InvalidOperationException("Jwt:Key is missing.");
    var jwtIssuer = jwtSection["Issuer"] ?? "ScheduleAnywhere";
    var jwtAudience = jwtSection["Audience"] ?? "ScheduleAnywhere";

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                ValidateIssuer = true,
                ValidIssuer = jwtIssuer,
                ValidateAudience = true,
                ValidAudience = jwtAudience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(1)
            };
        });

    builder.Services.AddAuthorization();

    // ── CORS ──────────────────────────────────────────────────────────────────
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("ReactDev", policy => policy
            .WithOrigins("http://localhost:5173", "https://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());

        options.AddPolicy("Production", policy => policy
            .WithOrigins(builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [])
            .AllowAnyHeader()
            .WithMethods("GET", "POST", "PUT", "DELETE")
            .AllowCredentials());
    });

    // ── OpenAPI (.NET 10 native) ───────────────────────────────────────────────
    builder.Services.AddOpenApi(options =>
    {
        options.AddDocumentTransformer((document, context, ct) =>
        {
            document.Info.Title = "ScheduleAnywhere API";
            document.Info.Version = "v1";
            document.Info.Description = "REST API for the ScheduleAnywhere workforce scheduling platform.";
            return Task.CompletedTask;
        });
    });

    // ── Exception handling ────────────────────────────────────────────────────
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    builder.Services.AddProblemDetails();

    // ── HTTP context ──────────────────────────────────────────────────────────
    builder.Services.AddHttpContextAccessor();

    // ── Repositories ─────────────────────────────────────────────────────────
    builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

    // ── Services ──────────────────────────────────────────────────────────────
    builder.Services.AddScoped<IAuthService, AuthService>();
    builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
    builder.Services.AddScoped<IEmployeeService, EmployeeService>();
    builder.Services.AddScoped<IScheduleService, ScheduleService>();
    builder.Services.AddScoped<IScheduleItemService, ScheduleItemService>();
    builder.Services.AddScoped<IShiftService, ShiftService>();
    builder.Services.AddScoped<IExplanationService, ExplanationService>();
    builder.Services.AddScoped<IDepartmentService, DepartmentService>();
    builder.Services.AddScoped<ILocationService, LocationService>();
    builder.Services.AddScoped<IPositionService, PositionService>();
    builder.Services.AddScoped<ISkillService, SkillService>();
    builder.Services.AddScoped<IFilterService, FilterService>();
    builder.Services.AddScoped<IWorkgroupService, WorkgroupService>();
    builder.Services.AddScoped<ICoverageWatchService, CoverageWatchService>();
    builder.Services.AddScoped<IOpenShiftService, OpenShiftService>();

    builder.Services.AddControllers();

    // ── Build ─────────────────────────────────────────────────────────────────
    var app = builder.Build();

    app.UseSerilogRequestLogging(opts =>
        opts.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms");

    app.UseExceptionHandler();

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();                   // GET /openapi/v1.json
        app.MapScalarApiReference(opts =>   // GET /scalar/v1  — interactive UI
        {
            opts.Title = "ScheduleAnywhere API";
            opts.DefaultHttpClient = new(ScalarTarget.JavaScript, ScalarClient.Fetch);
        });
        app.UseCors("ReactDev");
    }
    else
    {
        app.UseCors("Production");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    Log.Information("ScheduleAnywhere API starting on {Environment}", app.Environment.EnvironmentName);
    app.Run();
}
catch (Exception ex) when (ex is not HostAbortedException)
{
    Log.Fatal(ex, "Application startup failed");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}

return 0;
