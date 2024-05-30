using Microsoft.EntityFrameworkCore;
using Zadanie10.Context;

namespace Zadanie10.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly ClinicDbContext _context;
    
    public PrescriptionService(ClinicDbContext context)
    {
        _context = context;
    }
    
    public async Task<Patient> GetOrCreatePatient(Patient patient)
    {
        var existingPatient = await _context.Patients.FirstOrDefaultAsync(p => p.IdPatient == patient.IdPatient);
        if (existingPatient != null)
        {
            return existingPatient;
        }
        
        await _context.Patients.AddAsync(patient);
        await _context.SaveChangesAsync();
        return patient;
    }
    
    public async Task<bool> MedicamentsExist(IEnumerable<int> medicamentIds)
    {
        foreach (var medicamentId in medicamentIds)
        {
            var existingMedicament = await _context.Medicaments.FindAsync(medicamentId);
            if (existingMedicament == null)
            {
                return false;
            }
        }
        return true;
    }

    public void ValidatePrescription(Prescription prescription)
    {
        if (prescription.PrescriptionMedicaments.Count > 10)
        {
            throw new InvalidOperationException("Too many medicaments");
        }

        if (prescription.DueDate < prescription.Date)
        {
            throw new InvalidOperationException("Due date must be greater than date");
        }
    }

    public async Task<Prescription> AddPrescription(Prescription prescription)
    {
        ValidatePrescription(prescription);

        var patient = await GetOrCreatePatient(prescription.Patient);

        var medicamentsExist = await MedicamentsExist(prescription.PrescriptionMedicaments.Select(pm => pm.IdMedicament));
        if (!medicamentsExist)
        {
            throw new InvalidOperationException("Medicaments not found");
        }
        
        var doctor = await GetDoctor(prescription.IdDoctor);

        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();

        return prescription;
    }
    
    public async Task<Doctor> GetDoctor(int id)
    {
        var doctor = await _context.Doctors.FindAsync(id);

        if (doctor == null)
        {
            throw new InvalidOperationException("Doctor not found");
        }
        
        return doctor;
    }
    
}