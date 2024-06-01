using System.Runtime.InteropServices.JavaScript;

namespace ABPD10.Entities;

public class Patient
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly BirthDate { get; set; }
    public ICollection<Prescription> Prescriptions = new List<Prescription>();
}