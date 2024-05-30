using Microsoft.AspNetCore.Mvc;
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

    [HttpPost]
    public async Task<IActionResult> AddPrescription(Prescription prescription)
    {
        try
        {
            var addedPrescription = await _prescriptionService.AddPrescription(prescription);
            return Ok(addedPrescription);
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(e.Message);
        }
    }
}