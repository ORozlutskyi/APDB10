using ABPD10.Entities;

namespace ABPD10.DTOs;

public class AddPrescriptionDto
{
    public PatientDto PatientDto { get; set; }
    public DoctorDto DoctorDto { get; set; }
    public ICollection<MedicamentDto> MedicamentDtos { get; set; } = new List<MedicamentDto>();
    public string Date { get; set; }
    public string DueDate { get; set; }
}