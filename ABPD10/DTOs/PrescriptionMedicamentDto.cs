namespace ABPD10.DTOs;

public class PrescriptionMedicamentDto
{
    public int IdMedicament { get; set; }
    public int IdPrescription { get; set; }
    public int? Dose { get; set; }
    public string Details { get; set; }
}