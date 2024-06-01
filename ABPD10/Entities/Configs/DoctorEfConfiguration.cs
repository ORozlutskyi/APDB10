using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABPD10.Entities.Configs;

public class DoctorEfConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.HasKey(e => e.Id).HasName("IdDoctor");
        builder.Property(e => e.Id).ValueGeneratedNever();

        builder.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(e => e.LastName).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Email).IsRequired().HasMaxLength(100);

        builder.ToTable("Doctor");

        Doctor[] doctors =
        {
            new Doctor { Id = 1, FirstName = "1", LastName = "1", Email = "1"},
            new Doctor { Id = 2, FirstName = "2", LastName = "2", Email = "2"}
        };

        builder.HasData(doctors);
    }
}