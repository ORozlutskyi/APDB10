namespace ABPD10.Entities;

public class Medicament
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public ICollection<PrescriptionMedicament> PrescriptionMedicaments = new List<PrescriptionMedicament>();
}