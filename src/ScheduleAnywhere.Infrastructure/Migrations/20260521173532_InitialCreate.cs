using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScheduleAnywhere.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_organization",
                columns: table => new
                {
                    id_i = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name_nvc = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    code_nvc = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description_nvc = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status_i = table.Column<int>(type: "int", nullable: false),
                    active_b = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_dt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    lastmodifieddatetime_dt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_organization", x => x.id_i);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_role",
                columns: table => new
                {
                    id_i = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name_vc = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description_vc = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_role", x => x.id_i);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_shiftColor",
                columns: table => new
                {
                    id_i = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name_nvc = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hexcode_nvc = table.Column<string>(type: "varchar(7)", maxLength: 7, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_shiftColor", x => x.id_i);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_department",
                columns: table => new
                {
                    id_i = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    organization_id_i = table.Column<int>(type: "int", nullable: false),
                    name_nvc = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description_nvc = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    active_b = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    lastmodifieddatetime_dt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_department", x => x.id_i);
                    table.ForeignKey(
                        name: "FK_t_department_t_organization_organization_id_i",
                        column: x => x.organization_id_i,
                        principalTable: "t_organization",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_location",
                columns: table => new
                {
                    id_i = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    organization_id_i = table.Column<int>(type: "int", nullable: false),
                    name_nvc = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description_nvc = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    active_b = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    lastmodifieddatetime_dt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_location", x => x.id_i);
                    table.ForeignKey(
                        name: "FK_t_location_t_organization_organization_id_i",
                        column: x => x.organization_id_i,
                        principalTable: "t_organization",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_position",
                columns: table => new
                {
                    id_i = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    organization_id_i = table.Column<int>(type: "int", nullable: false),
                    name_nvc = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description_nvc = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    active_b = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    lastmodifieddatetime_dt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_position", x => x.id_i);
                    table.ForeignKey(
                        name: "FK_t_position_t_organization_organization_id_i",
                        column: x => x.organization_id_i,
                        principalTable: "t_organization",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_schedule",
                columns: table => new
                {
                    id_i = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    organization_id_i = table.Column<int>(type: "int", nullable: false),
                    name_nvc = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description_nvc = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    active_b = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    isposted_b = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    posteddate_dt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    lastmodifieddatetime_dt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_schedule", x => x.id_i);
                    table.ForeignKey(
                        name: "FK_t_schedule_t_organization_organization_id_i",
                        column: x => x.organization_id_i,
                        principalTable: "t_organization",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_skill",
                columns: table => new
                {
                    id_i = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    organization_id_i = table.Column<int>(type: "int", nullable: false),
                    name_nvc = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description_nvc = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    active_b = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    lastmodifieddatetime_dt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_skill", x => x.id_i);
                    table.ForeignKey(
                        name: "FK_t_skill_t_organization_organization_id_i",
                        column: x => x.organization_id_i,
                        principalTable: "t_organization",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_employee",
                columns: table => new
                {
                    id_i = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    organization_id_i = table.Column<int>(type: "int", nullable: false),
                    department_id_i = table.Column<int>(type: "int", nullable: false),
                    location_id_i = table.Column<int>(type: "int", nullable: false),
                    position_id_i = table.Column<int>(type: "int", nullable: false),
                    supervisor_employee_id_i = table.Column<int>(type: "int", nullable: true),
                    firstname_nvc = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    lastname_nvc = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    username_nvc = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password_nvc = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password_salt_nvc = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email1_nvc = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email2_nvc = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone1_nvc = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mobile_nvc = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ssn_nvc = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    exportidentifier_nvc = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    comments_nvc = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    logintoken_nvc = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    resetpasswordtoken_nvc = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    active_b = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    canlogin_b = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_dt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    hiredate_dt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deactivated_dt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    lastlogin_dt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    lastmodifieddatetime_dt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    cost_sm = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: true),
                    costtype_i = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_employee", x => x.id_i);
                    table.ForeignKey(
                        name: "FK_t_employee_t_department_department_id_i",
                        column: x => x.department_id_i,
                        principalTable: "t_department",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_employee_t_employee_supervisor_employee_id_i",
                        column: x => x.supervisor_employee_id_i,
                        principalTable: "t_employee",
                        principalColumn: "id_i");
                    table.ForeignKey(
                        name: "FK_t_employee_t_location_location_id_i",
                        column: x => x.location_id_i,
                        principalTable: "t_location",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_employee_t_organization_organization_id_i",
                        column: x => x.organization_id_i,
                        principalTable: "t_organization",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_employee_t_position_position_id_i",
                        column: x => x.position_id_i,
                        principalTable: "t_position",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_coverageWatch",
                columns: table => new
                {
                    id_i = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    schedule_id_i = table.Column<int>(type: "int", nullable: false),
                    name_nvc = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    active_b = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    lastmodifieddatetime_dt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_coverageWatch", x => x.id_i);
                    table.ForeignKey(
                        name: "FK_t_coverageWatch_t_schedule_schedule_id_i",
                        column: x => x.schedule_id_i,
                        principalTable: "t_schedule",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_explanation",
                columns: table => new
                {
                    id_i = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    schedule_id_i = table.Column<int>(type: "int", nullable: false),
                    name_nvc = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    abbreviation_nvc = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description_nvc = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    active_b = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    istimeoff_b = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    lastmodifieddatetime_dt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_explanation", x => x.id_i);
                    table.ForeignKey(
                        name: "FK_t_explanation_t_schedule_schedule_id_i",
                        column: x => x.schedule_id_i,
                        principalTable: "t_schedule",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_filter",
                columns: table => new
                {
                    id_i = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    schedule_id_i = table.Column<int>(type: "int", nullable: false),
                    name_nvc = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description_nvc = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    active_b = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    lastmodifieddatetime_dt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_filter", x => x.id_i);
                    table.ForeignKey(
                        name: "FK_t_filter_t_schedule_schedule_id_i",
                        column: x => x.schedule_id_i,
                        principalTable: "t_schedule",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_shift",
                columns: table => new
                {
                    id_i = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    schedule_id_i = table.Column<int>(type: "int", nullable: false),
                    color_id_i = table.Column<int>(type: "int", nullable: false),
                    parentshift_id_i = table.Column<int>(type: "int", nullable: true),
                    shifttag_id_i = table.Column<int>(type: "int", nullable: true),
                    timeofftag_id_i = table.Column<int>(type: "int", nullable: true),
                    name_nvc = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    abbreviation_nvc = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description_nvc = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    code_nvc = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    active_b = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    istimeoff_b = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    shiftview_i = table.Column<int>(type: "int", nullable: false),
                    starttime_dt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    stoptime_dt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    breakminutes_i = table.Column<int>(type: "int", nullable: false),
                    paidhours_m = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: true),
                    unpaidhours_m = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: true),
                    lastmodifieddatetime_dt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_shift", x => x.id_i);
                    table.ForeignKey(
                        name: "FK_t_shift_t_schedule_schedule_id_i",
                        column: x => x.schedule_id_i,
                        principalTable: "t_schedule",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_shift_t_shiftColor_color_id_i",
                        column: x => x.color_id_i,
                        principalTable: "t_shiftColor",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_shift_t_shift_parentshift_id_i",
                        column: x => x.parentshift_id_i,
                        principalTable: "t_shift",
                        principalColumn: "id_i");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_workgroup",
                columns: table => new
                {
                    id_i = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    schedule_id_i = table.Column<int>(type: "int", nullable: false),
                    name_nvc = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description_nvc = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    active_b = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    lastmodifieddatetime_dt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_workgroup", x => x.id_i);
                    table.ForeignKey(
                        name: "FK_t_workgroup_t_schedule_schedule_id_i",
                        column: x => x.schedule_id_i,
                        principalTable: "t_schedule",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_mapRoleEmployee",
                columns: table => new
                {
                    employee_id_i = table.Column<int>(type: "int", nullable: false),
                    role_id_i = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_mapRoleEmployee", x => new { x.employee_id_i, x.role_id_i });
                    table.ForeignKey(
                        name: "FK_t_mapRoleEmployee_t_employee_employee_id_i",
                        column: x => x.employee_id_i,
                        principalTable: "t_employee",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_mapRoleEmployee_t_role_role_id_i",
                        column: x => x.role_id_i,
                        principalTable: "t_role",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_mapScheduleEmployeeAccess",
                columns: table => new
                {
                    employee_id_i = table.Column<int>(type: "int", nullable: false),
                    schedule_id_i = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_mapScheduleEmployeeAccess", x => new { x.employee_id_i, x.schedule_id_i });
                    table.ForeignKey(
                        name: "FK_t_mapScheduleEmployeeAccess_t_employee_employee_id_i",
                        column: x => x.employee_id_i,
                        principalTable: "t_employee",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_mapScheduleEmployeeAccess_t_schedule_schedule_id_i",
                        column: x => x.schedule_id_i,
                        principalTable: "t_schedule",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_mapSkillEmployee",
                columns: table => new
                {
                    skill_id_i = table.Column<int>(type: "int", nullable: false),
                    employee_id_i = table.Column<int>(type: "int", nullable: false),
                    expirationdate_dt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_mapSkillEmployee", x => new { x.skill_id_i, x.employee_id_i });
                    table.ForeignKey(
                        name: "FK_t_mapSkillEmployee_t_employee_employee_id_i",
                        column: x => x.employee_id_i,
                        principalTable: "t_employee",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_mapSkillEmployee_t_skill_skill_id_i",
                        column: x => x.skill_id_i,
                        principalTable: "t_skill",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_mapCoverageWatchDepartment",
                columns: table => new
                {
                    coveragewatch_id_i = table.Column<int>(type: "int", nullable: false),
                    department_id_i = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_mapCoverageWatchDepartment", x => new { x.coveragewatch_id_i, x.department_id_i });
                    table.ForeignKey(
                        name: "FK_t_mapCoverageWatchDepartment_t_coverageWatch_coveragewatch_i~",
                        column: x => x.coveragewatch_id_i,
                        principalTable: "t_coverageWatch",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_mapCoverageWatchDepartment_t_department_department_id_i",
                        column: x => x.department_id_i,
                        principalTable: "t_department",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_mapCoverageWatchPosition",
                columns: table => new
                {
                    coveragewatch_id_i = table.Column<int>(type: "int", nullable: false),
                    position_id_i = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_mapCoverageWatchPosition", x => new { x.coveragewatch_id_i, x.position_id_i });
                    table.ForeignKey(
                        name: "FK_t_mapCoverageWatchPosition_t_coverageWatch_coveragewatch_id_i",
                        column: x => x.coveragewatch_id_i,
                        principalTable: "t_coverageWatch",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_mapCoverageWatchPosition_t_position_position_id_i",
                        column: x => x.position_id_i,
                        principalTable: "t_position",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_requirementRow",
                columns: table => new
                {
                    id_i = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    coveragewatch_id_i = table.Column<int>(type: "int", nullable: false),
                    dayofweek_i = table.Column<int>(type: "int", nullable: false),
                    minimumcount_i = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_requirementRow", x => x.id_i);
                    table.ForeignKey(
                        name: "FK_t_requirementRow_t_coverageWatch_coveragewatch_id_i",
                        column: x => x.coveragewatch_id_i,
                        principalTable: "t_coverageWatch",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_mapFilterDepartment",
                columns: table => new
                {
                    filter_id_i = table.Column<int>(type: "int", nullable: false),
                    department_id_i = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_mapFilterDepartment", x => new { x.filter_id_i, x.department_id_i });
                    table.ForeignKey(
                        name: "FK_t_mapFilterDepartment_t_department_department_id_i",
                        column: x => x.department_id_i,
                        principalTable: "t_department",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_mapFilterDepartment_t_filter_filter_id_i",
                        column: x => x.filter_id_i,
                        principalTable: "t_filter",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_mapFilterEmployee",
                columns: table => new
                {
                    filter_id_i = table.Column<int>(type: "int", nullable: false),
                    employee_id_i = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_mapFilterEmployee", x => new { x.filter_id_i, x.employee_id_i });
                    table.ForeignKey(
                        name: "FK_t_mapFilterEmployee_t_employee_employee_id_i",
                        column: x => x.employee_id_i,
                        principalTable: "t_employee",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_mapFilterEmployee_t_filter_filter_id_i",
                        column: x => x.filter_id_i,
                        principalTable: "t_filter",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_mapFilterExplanation",
                columns: table => new
                {
                    filter_id_i = table.Column<int>(type: "int", nullable: false),
                    explanation_id_i = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_mapFilterExplanation", x => new { x.filter_id_i, x.explanation_id_i });
                    table.ForeignKey(
                        name: "FK_t_mapFilterExplanation_t_explanation_explanation_id_i",
                        column: x => x.explanation_id_i,
                        principalTable: "t_explanation",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_mapFilterExplanation_t_filter_filter_id_i",
                        column: x => x.filter_id_i,
                        principalTable: "t_filter",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_mapFilterLocation",
                columns: table => new
                {
                    filter_id_i = table.Column<int>(type: "int", nullable: false),
                    location_id_i = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_mapFilterLocation", x => new { x.filter_id_i, x.location_id_i });
                    table.ForeignKey(
                        name: "FK_t_mapFilterLocation_t_filter_filter_id_i",
                        column: x => x.filter_id_i,
                        principalTable: "t_filter",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_mapFilterLocation_t_location_location_id_i",
                        column: x => x.location_id_i,
                        principalTable: "t_location",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_mapFilterPosition",
                columns: table => new
                {
                    filter_id_i = table.Column<int>(type: "int", nullable: false),
                    position_id_i = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_mapFilterPosition", x => new { x.filter_id_i, x.position_id_i });
                    table.ForeignKey(
                        name: "FK_t_mapFilterPosition_t_filter_filter_id_i",
                        column: x => x.filter_id_i,
                        principalTable: "t_filter",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_mapFilterPosition_t_position_position_id_i",
                        column: x => x.position_id_i,
                        principalTable: "t_position",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_mapCoverageWatchShift",
                columns: table => new
                {
                    coveragewatch_id_i = table.Column<int>(type: "int", nullable: false),
                    shift_id_i = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_mapCoverageWatchShift", x => new { x.coveragewatch_id_i, x.shift_id_i });
                    table.ForeignKey(
                        name: "FK_t_mapCoverageWatchShift_t_coverageWatch_coveragewatch_id_i",
                        column: x => x.coveragewatch_id_i,
                        principalTable: "t_coverageWatch",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_mapCoverageWatchShift_t_shift_shift_id_i",
                        column: x => x.shift_id_i,
                        principalTable: "t_shift",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_mapFilterShift",
                columns: table => new
                {
                    filter_id_i = table.Column<int>(type: "int", nullable: false),
                    shift_id_i = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_mapFilterShift", x => new { x.filter_id_i, x.shift_id_i });
                    table.ForeignKey(
                        name: "FK_t_mapFilterShift_t_filter_filter_id_i",
                        column: x => x.filter_id_i,
                        principalTable: "t_filter",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_mapFilterShift_t_shift_shift_id_i",
                        column: x => x.shift_id_i,
                        principalTable: "t_shift",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_openShift",
                columns: table => new
                {
                    id_i = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    schedule_id_i = table.Column<int>(type: "int", nullable: false),
                    shift_id_i = table.Column<int>(type: "int", nullable: false),
                    date_dt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    isactive_b = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    lastmodifieddatetime_dt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_openShift", x => x.id_i);
                    table.ForeignKey(
                        name: "FK_t_openShift_t_schedule_schedule_id_i",
                        column: x => x.schedule_id_i,
                        principalTable: "t_schedule",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_openShift_t_shift_shift_id_i",
                        column: x => x.shift_id_i,
                        principalTable: "t_shift",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_scheduleItem",
                columns: table => new
                {
                    id_i = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    schedule_id_i = table.Column<int>(type: "int", nullable: false),
                    employee_id_i = table.Column<int>(type: "int", nullable: false),
                    shift_id_i = table.Column<int>(type: "int", nullable: true),
                    explanation_id_i = table.Column<int>(type: "int", nullable: true),
                    date_dt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    notes_nvc = table.Column<string>(type: "varchar(4000)", maxLength: 4000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    paidhours_m = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: true),
                    unpaidhours_m = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: true),
                    lastmodifieddatetime_dt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_scheduleItem", x => x.id_i);
                    table.ForeignKey(
                        name: "FK_t_scheduleItem_t_employee_employee_id_i",
                        column: x => x.employee_id_i,
                        principalTable: "t_employee",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_scheduleItem_t_explanation_explanation_id_i",
                        column: x => x.explanation_id_i,
                        principalTable: "t_explanation",
                        principalColumn: "id_i");
                    table.ForeignKey(
                        name: "FK_t_scheduleItem_t_schedule_schedule_id_i",
                        column: x => x.schedule_id_i,
                        principalTable: "t_schedule",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_scheduleItem_t_shift_shift_id_i",
                        column: x => x.shift_id_i,
                        principalTable: "t_shift",
                        principalColumn: "id_i");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_mapWorkgroupDepartment",
                columns: table => new
                {
                    workgroup_id_i = table.Column<int>(type: "int", nullable: false),
                    department_id_i = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_mapWorkgroupDepartment", x => new { x.workgroup_id_i, x.department_id_i });
                    table.ForeignKey(
                        name: "FK_t_mapWorkgroupDepartment_t_department_department_id_i",
                        column: x => x.department_id_i,
                        principalTable: "t_department",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_mapWorkgroupDepartment_t_workgroup_workgroup_id_i",
                        column: x => x.workgroup_id_i,
                        principalTable: "t_workgroup",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_mapWorkgroupEmployee",
                columns: table => new
                {
                    workgroup_id_i = table.Column<int>(type: "int", nullable: false),
                    employee_id_i = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_mapWorkgroupEmployee", x => new { x.workgroup_id_i, x.employee_id_i });
                    table.ForeignKey(
                        name: "FK_t_mapWorkgroupEmployee_t_employee_employee_id_i",
                        column: x => x.employee_id_i,
                        principalTable: "t_employee",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_mapWorkgroupEmployee_t_workgroup_workgroup_id_i",
                        column: x => x.workgroup_id_i,
                        principalTable: "t_workgroup",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_mapWorkgroupLocation",
                columns: table => new
                {
                    workgroup_id_i = table.Column<int>(type: "int", nullable: false),
                    location_id_i = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_mapWorkgroupLocation", x => new { x.workgroup_id_i, x.location_id_i });
                    table.ForeignKey(
                        name: "FK_t_mapWorkgroupLocation_t_location_location_id_i",
                        column: x => x.location_id_i,
                        principalTable: "t_location",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_mapWorkgroupLocation_t_workgroup_workgroup_id_i",
                        column: x => x.workgroup_id_i,
                        principalTable: "t_workgroup",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_mapWorkgroupPosition",
                columns: table => new
                {
                    workgroup_id_i = table.Column<int>(type: "int", nullable: false),
                    position_id_i = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_mapWorkgroupPosition", x => new { x.workgroup_id_i, x.position_id_i });
                    table.ForeignKey(
                        name: "FK_t_mapWorkgroupPosition_t_position_position_id_i",
                        column: x => x.position_id_i,
                        principalTable: "t_position",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_mapWorkgroupPosition_t_workgroup_workgroup_id_i",
                        column: x => x.workgroup_id_i,
                        principalTable: "t_workgroup",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_mapWorkgroupSkill",
                columns: table => new
                {
                    workgroup_id_i = table.Column<int>(type: "int", nullable: false),
                    skill_id_i = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_mapWorkgroupSkill", x => new { x.workgroup_id_i, x.skill_id_i });
                    table.ForeignKey(
                        name: "FK_t_mapWorkgroupSkill_t_skill_skill_id_i",
                        column: x => x.skill_id_i,
                        principalTable: "t_skill",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_mapWorkgroupSkill_t_workgroup_workgroup_id_i",
                        column: x => x.workgroup_id_i,
                        principalTable: "t_workgroup",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_openShiftRequest",
                columns: table => new
                {
                    id_i = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    openshift_id_i = table.Column<int>(type: "int", nullable: false),
                    employee_id_i = table.Column<int>(type: "int", nullable: false),
                    isapproved_b = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    notes_nvc = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    requestedat_dt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    reviewedat_dt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_openShiftRequest", x => x.id_i);
                    table.ForeignKey(
                        name: "FK_t_openShiftRequest_t_employee_employee_id_i",
                        column: x => x.employee_id_i,
                        principalTable: "t_employee",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_openShiftRequest_t_openShift_openshift_id_i",
                        column: x => x.openshift_id_i,
                        principalTable: "t_openShift",
                        principalColumn: "id_i",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_t_coverageWatch_schedule_id_i",
                table: "t_coverageWatch",
                column: "schedule_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_department_organization_id_i",
                table: "t_department",
                column: "organization_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_employee_department_id_i",
                table: "t_employee",
                column: "department_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_employee_location_id_i",
                table: "t_employee",
                column: "location_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_employee_organization_id_i_username_nvc",
                table: "t_employee",
                columns: new[] { "organization_id_i", "username_nvc" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_employee_position_id_i",
                table: "t_employee",
                column: "position_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_employee_supervisor_employee_id_i",
                table: "t_employee",
                column: "supervisor_employee_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_explanation_schedule_id_i",
                table: "t_explanation",
                column: "schedule_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_filter_schedule_id_i",
                table: "t_filter",
                column: "schedule_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_location_organization_id_i",
                table: "t_location",
                column: "organization_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_mapCoverageWatchDepartment_department_id_i",
                table: "t_mapCoverageWatchDepartment",
                column: "department_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_mapCoverageWatchPosition_position_id_i",
                table: "t_mapCoverageWatchPosition",
                column: "position_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_mapCoverageWatchShift_shift_id_i",
                table: "t_mapCoverageWatchShift",
                column: "shift_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_mapFilterDepartment_department_id_i",
                table: "t_mapFilterDepartment",
                column: "department_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_mapFilterEmployee_employee_id_i",
                table: "t_mapFilterEmployee",
                column: "employee_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_mapFilterExplanation_explanation_id_i",
                table: "t_mapFilterExplanation",
                column: "explanation_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_mapFilterLocation_location_id_i",
                table: "t_mapFilterLocation",
                column: "location_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_mapFilterPosition_position_id_i",
                table: "t_mapFilterPosition",
                column: "position_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_mapFilterShift_shift_id_i",
                table: "t_mapFilterShift",
                column: "shift_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_mapRoleEmployee_role_id_i",
                table: "t_mapRoleEmployee",
                column: "role_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_mapScheduleEmployeeAccess_schedule_id_i",
                table: "t_mapScheduleEmployeeAccess",
                column: "schedule_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_mapSkillEmployee_employee_id_i",
                table: "t_mapSkillEmployee",
                column: "employee_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_mapWorkgroupDepartment_department_id_i",
                table: "t_mapWorkgroupDepartment",
                column: "department_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_mapWorkgroupEmployee_employee_id_i",
                table: "t_mapWorkgroupEmployee",
                column: "employee_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_mapWorkgroupLocation_location_id_i",
                table: "t_mapWorkgroupLocation",
                column: "location_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_mapWorkgroupPosition_position_id_i",
                table: "t_mapWorkgroupPosition",
                column: "position_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_mapWorkgroupSkill_skill_id_i",
                table: "t_mapWorkgroupSkill",
                column: "skill_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_openShift_schedule_id_i",
                table: "t_openShift",
                column: "schedule_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_openShift_shift_id_i",
                table: "t_openShift",
                column: "shift_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_openShiftRequest_employee_id_i",
                table: "t_openShiftRequest",
                column: "employee_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_openShiftRequest_openshift_id_i",
                table: "t_openShiftRequest",
                column: "openshift_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_position_organization_id_i",
                table: "t_position",
                column: "organization_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_requirementRow_coveragewatch_id_i",
                table: "t_requirementRow",
                column: "coveragewatch_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_schedule_organization_id_i",
                table: "t_schedule",
                column: "organization_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_scheduleItem_employee_id_i",
                table: "t_scheduleItem",
                column: "employee_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_scheduleItem_explanation_id_i",
                table: "t_scheduleItem",
                column: "explanation_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_scheduleItem_schedule_id_i",
                table: "t_scheduleItem",
                column: "schedule_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_scheduleItem_shift_id_i",
                table: "t_scheduleItem",
                column: "shift_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_shift_color_id_i",
                table: "t_shift",
                column: "color_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_shift_parentshift_id_i",
                table: "t_shift",
                column: "parentshift_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_shift_schedule_id_i",
                table: "t_shift",
                column: "schedule_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_skill_organization_id_i",
                table: "t_skill",
                column: "organization_id_i");

            migrationBuilder.CreateIndex(
                name: "IX_t_workgroup_schedule_id_i",
                table: "t_workgroup",
                column: "schedule_id_i");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_mapCoverageWatchDepartment");

            migrationBuilder.DropTable(
                name: "t_mapCoverageWatchPosition");

            migrationBuilder.DropTable(
                name: "t_mapCoverageWatchShift");

            migrationBuilder.DropTable(
                name: "t_mapFilterDepartment");

            migrationBuilder.DropTable(
                name: "t_mapFilterEmployee");

            migrationBuilder.DropTable(
                name: "t_mapFilterExplanation");

            migrationBuilder.DropTable(
                name: "t_mapFilterLocation");

            migrationBuilder.DropTable(
                name: "t_mapFilterPosition");

            migrationBuilder.DropTable(
                name: "t_mapFilterShift");

            migrationBuilder.DropTable(
                name: "t_mapRoleEmployee");

            migrationBuilder.DropTable(
                name: "t_mapScheduleEmployeeAccess");

            migrationBuilder.DropTable(
                name: "t_mapSkillEmployee");

            migrationBuilder.DropTable(
                name: "t_mapWorkgroupDepartment");

            migrationBuilder.DropTable(
                name: "t_mapWorkgroupEmployee");

            migrationBuilder.DropTable(
                name: "t_mapWorkgroupLocation");

            migrationBuilder.DropTable(
                name: "t_mapWorkgroupPosition");

            migrationBuilder.DropTable(
                name: "t_mapWorkgroupSkill");

            migrationBuilder.DropTable(
                name: "t_openShiftRequest");

            migrationBuilder.DropTable(
                name: "t_requirementRow");

            migrationBuilder.DropTable(
                name: "t_scheduleItem");

            migrationBuilder.DropTable(
                name: "t_filter");

            migrationBuilder.DropTable(
                name: "t_role");

            migrationBuilder.DropTable(
                name: "t_skill");

            migrationBuilder.DropTable(
                name: "t_workgroup");

            migrationBuilder.DropTable(
                name: "t_openShift");

            migrationBuilder.DropTable(
                name: "t_coverageWatch");

            migrationBuilder.DropTable(
                name: "t_employee");

            migrationBuilder.DropTable(
                name: "t_explanation");

            migrationBuilder.DropTable(
                name: "t_shift");

            migrationBuilder.DropTable(
                name: "t_department");

            migrationBuilder.DropTable(
                name: "t_location");

            migrationBuilder.DropTable(
                name: "t_position");

            migrationBuilder.DropTable(
                name: "t_schedule");

            migrationBuilder.DropTable(
                name: "t_shiftColor");

            migrationBuilder.DropTable(
                name: "t_organization");
        }
    }
}
