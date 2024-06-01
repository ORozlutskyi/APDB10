using System.Runtime.InteropServices.JavaScript;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABPD10.Entities.Configs;

public class PatientEfConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.HasKey(e => e.Id).HasName("IdPatient");
        builder.Property(e => e.Id).ValueGeneratedNever();

        builder.Property(e => e.FirstName).HasMaxLength(100);
        builder.Property(e => e.LastName).HasMaxLength(100);

        builder.ToTable("Patient");

        Patient[] patients =
        {
            new Patient{ Id = 1, FirstName = "Oleksandr", LastName = "Rozlutskyi", BirthDate = new DateOnly(2005, 2, 13)},
            new Patient{ Id = 2, FirstName = "Maryia", LastName = "Krasner", BirthDate = new DateOnly(2004, 6, 6)}
        };

        builder.HasData(patients);
    }
}