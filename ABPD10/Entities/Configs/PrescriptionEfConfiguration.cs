using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABPD10.Entities.Configs;

public class PrescriptionEfConfiguration : IEntityTypeConfiguration<Prescription>
{
    public void Configure(EntityTypeBuilder<Prescription> builder)
    {
        builder.HasKey(e => e.Id).HasName("IdPrescription");
        builder.Property(e => e.Id).ValueGeneratedNever();
        
        builder
            .HasOne(e => e.Doctor)
            .WithMany(e => e.Prescriptions)
            .HasForeignKey(e => e.IdDoctor)
            .HasConstraintName("IdDoctor_fk")
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(e => e.Patient)
            .WithMany(e => e.Prescriptions)
            .HasForeignKey(e => e.IdPatient)
            .HasConstraintName("IdPatient_fk")
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable("Prescription");

        Prescription[] prescriptions =
        {
            new Prescription
            {
                Id = 1, 
                Date = new DateOnly(2024, 3, 5), 
                DueDate = new DateOnly(2024, 3, 25), 
                IdDoctor = 1,
                IdPatient = 1
            },
            new Prescription
            {
                Id = 2, 
                Date = new DateOnly(2024, 3, 3), 
                DueDate = new DateOnly(2024, 3, 27), 
                IdDoctor = 2,
                IdPatient = 2
            }
        };

        builder.HasData(prescriptions);
    }
}