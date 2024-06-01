using ABPD10.DTOs;
using ABPD10.Entities;

namespace ABPD10.Repositories;

public interface IHospitalRepository
{
    Task<IEnumerable<Patient>> GetPatientsAsync();
    Task ValidateDoctorExistsAsync(DoctorDto doctor);
    void ValidateDate(string date, string dueDate);
    void ValidateNumberOfMedicaments(ICollection<MedicamentDto> medicaments);
    Task ValidateMedicamentsAsync(ICollection<MedicamentDto> medicaments);
    Task AddPatientIfNotExistsAsync(PatientDto patient);
    Task<Prescription> AddPrescriptionAsync(AddPrescriptionDto addPrescriptionDto);
    Task AddPrescriptionMedicamentsRecordsAsync(Prescription prescription, ICollection<MedicamentDto> medicamentDtos);
    Task<ICollection<PrescriptionDto>> GetPrescriptionDtosForPatient(int idPatient);
    Task<PatientPrescriptionDto> ReturnPatientPrescriptions(int idPatient, ICollection<PrescriptionDto> prescriptions);
}