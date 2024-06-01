using ABPD10.DTOs;
using ABPD10.Entities;
using ABPD10.Services;
using Microsoft.AspNetCore.Mvc;

namespace ABPD10.Controllers;
[Route("api/hospital")]
[ApiController]
public class HospitalController(IHospitalService hospitalService) : ControllerBase
{
    [HttpPost("addReceipt")]
    public async Task<IActionResult> AddReceiptAsync(AddPrescriptionDto addPrescriptionDto)
    {
        await hospitalService.AddReceiptAsync(addPrescriptionDto);
        
        return Ok("Receipt was added");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPrescriptionsForPatient(int id)
    {
        PatientPrescriptionDto patientPrescriptionDto = await hospitalService.GetPrescriptionsForPatient(id);

        return Ok(patientPrescriptionDto);
    }
    
    [HttpGet("patients")]
    public async Task<IActionResult> GetPatientsAsync()
    {
        IEnumerable<Patient> patients = await hospitalService.GetPatientsAsync();
        return Ok(patients);
    }
}