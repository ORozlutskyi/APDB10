using ABPD10.Entities;

namespace ABPD10.DTOs;

public class PrescriptionDto
{
    public int IdPrescription { get; set; }
    public DateOnly Date { get; set; }
    public DateOnly DueDate { get; set; }
    public ICollection<PrescriptionMedicamentDto> PrescriptionMedicamentsDtos { get; set; }
    public DoctorDto Doctor { get; set; }
}