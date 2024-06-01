using System.Collections;
using ABPD10.DTOs;
using ABPD10.Entities;
using ABPD10.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ABPD10.Repositories;

public class HospitalRepository(HospitalDbContext hospitalDbContext) : IHospitalRepository
{
    public async Task<IEnumerable<Patient>> GetPatientsAsync()
    {
        return await hospitalDbContext.Patients.ToListAsync();
    }

    public async Task ValidateDoctorExistsAsync(DoctorDto doctorDto)
    {
        bool exists = await hospitalDbContext.Doctors
            .AnyAsync(x => x.Id == doctorDto.Id);

        if (!exists)
        {
            throw new NoSuchDoctorException();
        }
    }

    public void ValidateDate(string date, string dueDate)
    {
        if (DateOnly.Parse(dueDate) < DateOnly.Parse(date)) throw new WrongDateException();
    }

    public void ValidateNumberOfMedicaments(ICollection<MedicamentDto> medicaments)
    {
        if (medicaments.Count > 10) throw new TooMuchMedicamentsException();
    }

    public async Task ValidateMedicamentsAsync(ICollection<MedicamentDto> medicaments)
    {
        foreach (var med in medicaments)
        {
            var exists = await hospitalDbContext.Medicaments.AnyAsync(x => x.Id == med.Id);

            if (!exists) throw new NoSuchMedicamentException();
        }
    }

    public async Task AddPatientIfNotExistsAsync(PatientDto patientDto)
    {
        var exists = await hospitalDbContext.Patients
            .AnyAsync(x => x.Id == patientDto.Id);

        if (!exists)
        {
            Patient patient = new Patient
            {
                Id = patientDto.Id,
                FirstName = patientDto.FirstName,
                LastName = patientDto.LastName,
                BirthDate = DateOnly.Parse(patientDto.BirthDate)
            };
            await hospitalDbContext.Patients.AddAsync(patient);
            await hospitalDbContext.SaveChangesAsync();
        }
    }

    public async Task<Prescription> AddPrescriptionAsync(AddPrescriptionDto addPrescriptionDto)
    {
        int maxId = await hospitalDbContext.Prescriptions.MaxAsync(x => x.Id) + 1;
        
        Prescription newPrescription = new Prescription
        {
            Id = maxId,
            Date = DateOnly.Parse(addPrescriptionDto.Date),
            DueDate = DateOnly.Parse(addPrescriptionDto.DueDate),
            IdPatient = addPrescriptionDto.PatientDto.Id,
            IdDoctor = addPrescriptionDto.DoctorDto.Id,
        };

        await hospitalDbContext.Prescriptions.AddAsync(newPrescription);
        await hospitalDbContext.SaveChangesAsync();
        
        return newPrescription;
    }

    public async Task AddPrescriptionMedicamentsRecordsAsync(Prescription prescription, ICollection<MedicamentDto> medicamentDtos)
    {
        foreach (var medDto in medicamentDtos)
        {
            PrescriptionMedicament prescriptionMedicament = new PrescriptionMedicament
            {
                IdPrescription = prescription.Id,
                IdMedicament = medDto.Id,
                Dose = medDto.Dose,
                Details = medDto.Description
            };

            await hospitalDbContext.PrescriptionMedicaments.AddAsync(prescriptionMedicament);
            await hospitalDbContext.SaveChangesAsync();
        }
    }

    public async Task<ICollection<PrescriptionDto>> GetPrescriptionDtosForPatient(int idPatient)
    {
        ICollection<PrescriptionDto> prescriptions = new List<PrescriptionDto>();

        var patientPrescriptions = 
            await hospitalDbContext.Prescriptions
            .Where(x => x.IdPatient == idPatient)
            .Include(p => p.Doctor)
            .ToListAsync();

        foreach (var prescription in patientPrescriptions)
        {
            var prescriptionMedicaments = await hospitalDbContext.PrescriptionMedicaments
                .Where(x => x.IdPrescription == prescription.Id)
                .ToListAsync();

            ICollection<PrescriptionMedicamentDto> prescriptionMedicamentDtos = new List<PrescriptionMedicamentDto>();

            foreach (var presMed in prescriptionMedicaments)
            {
                PrescriptionMedicamentDto prescriptionMedicamentDto = new PrescriptionMedicamentDto
                {
                    IdPrescription = presMed.IdPrescription,
                    IdMedicament = presMed.IdMedicament,
                    Details = presMed.Details,
                    Dose = presMed.Dose
                };
                
                prescriptionMedicamentDtos.Add(prescriptionMedicamentDto);
            }
            
            PrescriptionDto prescriptionDto = new PrescriptionDto
            {
                IdPrescription = prescription.Id,
                Date = prescription.Date,
                DueDate = prescription.DueDate,
                Doctor = DoctorToDoctorDto(prescription.Doctor),
                PrescriptionMedicamentsDtos = prescriptionMedicamentDtos
            };
            
            prescriptions.Add(prescriptionDto);
        }

        return prescriptions;
    }

    private DoctorDto DoctorToDoctorDto(Doctor doctor)
    {
        DoctorDto doctorDto = new DoctorDto
        {
            Id = doctor.Id,
            FirstName = doctor.FirstName,
            LastName = doctor.LastName,
            Email = doctor.Email
        };

        return doctorDto;
    }

    public async Task<PatientPrescriptionDto> ReturnPatientPrescriptions(int idPatient, ICollection<PrescriptionDto> prescriptions)
    {
        Patient patient = (await hospitalDbContext.Patients.FindAsync(idPatient))!;

        PatientPrescriptionDto patientPrescriptionDto = new PatientPrescriptionDto
        {
            IdPatient = patient.Id,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            BirthDate = patient.BirthDate,
            Prescriptions = prescriptions
        };

        return patientPrescriptionDto;
    }
}