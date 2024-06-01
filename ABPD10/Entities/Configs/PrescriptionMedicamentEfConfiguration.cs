using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABPD10.Entities.Configs;

public class PrescriptionMedicamentEfConfiguration : IEntityTypeConfiguration<PrescriptionMedicament>
{
    public void Configure(EntityTypeBuilder<PrescriptionMedicament> builder)
    {
        builder
            .HasKey(e => new { e.IdPrescription, e.IdMedicament })
            .HasName("PrescriptionMedicament_pk");

        builder.Property(e => e.Dose).IsRequired(false);
        builder.Property(e => e.Details).IsRequired().HasMaxLength(100);

        builder
            .HasOne(e => e.Medicament)
            .WithMany(e => e.PrescriptionMedicaments)
            .HasForeignKey(e => e.IdMedicament)
            .HasConstraintName("IdMedicament_fk")
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasOne(e => e.Prescription)
            .WithMany(e => e.PrescriptionMedicaments)
            .HasForeignKey(e => e.IdPrescription)
            .HasConstraintName("IdPrescription_fk")
            .OnDelete(DeleteBehavior.Restrict);

        PrescriptionMedicament[] prescriptionMedicaments =
        {
            new PrescriptionMedicament { IdMedicament = 1, IdPrescription = 1, Dose = 2, Details = "111" },
            new PrescriptionMedicament { IdMedicament = 2, IdPrescription = 2, Dose = 4, Details = "222" },
        };

        builder.HasData(prescriptionMedicaments);
    }
}