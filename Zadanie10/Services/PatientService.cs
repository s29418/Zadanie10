using Microsoft.EntityFrameworkCore;
using Zadanie10.Context;

namespace Zadanie10.Services;

public class PatientService
{
    private readonly ClinicDbContext _context;

    public PatientService(ClinicDbContext context)
    {
        _context = context;
    }
    
    public async Task<Patient> GetPatientWithDetailsAsync(int id)
    {
        var patient = await _context.Patients
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.PrescriptionMedicaments)
            .ThenInclude(pm => pm.Medicament)
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.Doctor)
            .FirstOrDefaultAsync(p => p.IdPatient == id);

        if (patient != null)
        {
            patient.Prescriptions = patient.Prescriptions.OrderBy(pr => pr.DueDate).ToList();
        }

        return patient;
    }
    
    
}