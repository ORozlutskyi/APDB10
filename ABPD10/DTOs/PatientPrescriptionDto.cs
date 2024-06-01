namespace ABPD10.DTOs;

public class PatientPrescriptionDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly BirthDate { get; set; }
    public ICollection<PrescriptionDto> Prescriptions { get; set; }
}