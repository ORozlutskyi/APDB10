using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABPD10.Entities.Configs;

public class MedicamentEfConfiguration : IEntityTypeConfiguration<Medicament>
{
    public void Configure(EntityTypeBuilder<Medicament> builder)
    {
        builder.HasKey(e => e.Id).HasName("IdMedicament");
        builder.Property(e => e.Id).ValueGeneratedNever();
        
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100); //IsRequires(false)
        builder.Property(e => e.Description).IsRequired().HasMaxLength(100); 
        builder.Property(e => e.Type).IsRequired().HasMaxLength(100);

        builder.ToTable("Medicament");

        Medicament[] medicaments =
        {
            new Medicament { Id = 1, Name = "1", Description = "1", Type = "1" },
            new Medicament { Id = 2, Name = "2", Description = "2", Type = "2" }
        };

        builder.HasData(medicaments);
    }
}