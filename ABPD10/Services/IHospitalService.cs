using ABPD10.DTOs;
using ABPD10.Entities;

namespace ABPD10.Services;

public interface IHospitalService
{
    Task AddReceiptAsync(AddPrescriptionDto addPrescriptionDto);
    Task<PatientPrescriptionDto> GetPrescriptionsForPatient(int idPatient);
    Task<IEnumerable<Patient>> GetPatientsAsync();
}