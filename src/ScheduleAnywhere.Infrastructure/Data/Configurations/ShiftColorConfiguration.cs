using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScheduleAnywhere.Core.Domain;

namespace ScheduleAnywhere.Infrastructure.Data.Configurations;

public class ShiftColorConfiguration : IEntityTypeConfiguration<ShiftColor>
{
    public void Configure(EntityTypeBuilder<ShiftColor> b)
    {
        b.ToTable("t_shiftColor");
        b.HasKey(c => c.Id);
        b.Property(c => c.Id).HasColumnName("id_i");
        b.Property(c => c.Name).HasColumnName("name_nvc").HasMaxLength(50);
        b.Property(c => c.HexCode).HasColumnName("hexcode_nvc").HasMaxLength(7);
    }
}
