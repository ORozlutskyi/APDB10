using ABPD10.DTOs;
using ABPD10.Entities;
using ABPD10.Repositories;

namespace ABPD10.Services;

public class HospitalService(IHospitalRepository hospitalRepository) : IHospitalService
{
    public async Task AddReceiptAsync(AddPrescriptionDto addPrescriptionDto)
    {
        // check for exceptions
        await hospitalRepository.ValidateDoctorExistsAsync(addPrescriptionDto.DoctorDto);
        hospitalRepository.ValidateDate(addPrescriptionDto.Date, addPrescriptionDto.DueDate);
        hospitalRepository.ValidateNumberOfMedicaments(addPrescriptionDto.MedicamentDtos);
        await hospitalRepository.ValidateMedicamentsAsync(addPrescriptionDto.MedicamentDtos);
        await hospitalRepository.AddPatientIfNotExistsAsync(addPrescriptionDto.PatientDto);
        
        // update database
        Prescription newPrescription = await hospitalRepository.AddPrescriptionAsync(addPrescriptionDto);
        await hospitalRepository.AddPrescriptionMedicamentsRecordsAsync(newPrescription, addPrescriptionDto.MedicamentDtos);
    }

    public async Task<PatientPrescriptionDto> GetPrescriptionsForPatient(int idPatient)
    {
        ICollection<PrescriptionDto> prescriptions = await hospitalRepository.GetPrescriptionDtosForPatient(idPatient);
        PatientPrescriptionDto patientPrescriptionDto = await hospitalRepository.ReturnPatientPrescriptions(idPatient, prescriptions);

        return patientPrescriptionDto;
    }

    public Task<IEnumerable<Patient>> GetPatientsAsync()
    {
        return hospitalRepository.GetPatientsAsync();
    }
}