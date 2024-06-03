using Microsoft.AspNetCore.Mvc;
using Zadanie10.DTO_s;
using Zadanie10.Services;

namespace Zadanie10.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionController : ControllerBase
{
    private readonly PrescriptionService _prescriptionService;

    public PrescriptionController(PrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    [HttpPost("{idDoctor}")]
    public async Task<IActionResult> AddPrescription(int idDoctor, [FromBody] PrescriptionRequest request)
    {
        try
        {
            var prescription = await _prescriptionService.AddPrescription(request, idDoctor);
            return Ok(prescription);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet("{idPatient}")]
    public async Task<ActionResult<PatientResponse>> GetPatient(int idPatient)
    {
        try
        {
            var patientDetails = await _prescriptionService.GetPatientDetails(idPatient);
            return Ok(patientDetails);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    
}
