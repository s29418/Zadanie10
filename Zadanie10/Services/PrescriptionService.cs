using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Zadanie10.Context;
using Zadanie10.DTO_s;
using Zadanie10.Exceptions;

namespace Zadanie10.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly ClinicDbContext _context;

    public PrescriptionService(ClinicDbContext context)
    {
        _context = context;
    }

    public async Task<Patient> GetOrCreatePatient(PatientRequest patientRequest)
    {
        var patient = await _context.Patients.FirstOrDefaultAsync(p => p.IdPatient == patientRequest.IdPatient);
        if (patient == null)
        {
            patient = new Patient
            {
                FirstName = patientRequest.FirstName,
                LastName = patientRequest.LastName,
                Birthdate = patientRequest.Birthdate
            };
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
        }
        return patient;
    }

    public async Task MedicamentsExist(IEnumerable<MedicamentRequest> medicaments)
    {
        // Get all medicament IDs from the request
        var medicamentIds = medicaments.Select(m => m.IdMedicament).ToList();

        // Get all existing medicament IDs from the database
        var existingMedicamentIds = await _context.Medicaments
            .Where(m => medicamentIds.Contains(m.IdMedicament))
            .Select(m => m.IdMedicament)
            .ToListAsync();

        // Check if any medicament ID from the request does not exist in the database
        var nonExistingMedicamentIds = medicamentIds.Except(existingMedicamentIds).ToList();
        if (nonExistingMedicamentIds.Any())
        {
            throw new NotFoundException($"Medicaments with ids {string.Join(", ", nonExistingMedicamentIds)} not found");
        }
    }
    
    public async Task<Prescription> GetPrescriptionById(int idPrescription)
    {
        var prescription = await _context.Prescriptions.FirstOrDefaultAsync(p => p.IdPrescription == idPrescription);

        if (prescription == null)
        {
            throw new NotFoundException($"Prescription with id {idPrescription} not found");
        }

        return prescription;
    }

    public void ValidatePrescription(PrescriptionRequest request)
    {
        if (request.Medicaments.Count > 10)
        {
            throw new InvalidOperationException("Too many medicaments");
        }

        if (request.DueDate < request.Date)
        {
            throw new InvalidOperationException("Due date must be greater than or equal to date");
        }
    }

    public async Task<Doctor> GetDoctorById(int idDoctor)
    {
        var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.IdDoctor == idDoctor);
        if (doctor == null)
        {
            throw new InvalidOperationException($"Doctor with ID {idDoctor} not found");
        }
        return doctor;
    }
    
    public async Task<Prescription> AddPrescription(PrescriptionRequest request, int idDoctor)
    {
        var patient = await GetOrCreatePatient(request.Patient);
        await MedicamentsExist(request.Medicaments);
        ValidatePrescription(request);
        var doctor = await GetDoctorById(idDoctor);

        var prescription = new Prescription
        {
            Date = request.Date,
            DueDate = request.DueDate,
            IdPatient = patient.IdPatient,
            IdDoctor = doctor.IdDoctor,
            PrescriptionMedicaments = request.Medicaments.Select(m => new PrescriptionMedicament
            {
                IdMedicament = m.IdMedicament,
                Dose = m.Dose,
                Details = m.Description
            }).ToList()
        };

        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();

        return prescription;
    }
    


    public async Task<Patient> GetPatientFromDatabase(int idPatient)
    {
        return await _context.Patients
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.PrescriptionMedicaments)
            .ThenInclude(pm => pm.Medicament)
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.Doctor)
            .FirstOrDefaultAsync(p => p.IdPatient == idPatient);
    }

    public PatientResponse MapPatientToResponse(Patient patient)
    {
        return new PatientResponse
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Birthdate = patient.Birthdate,
            Prescriptions = patient.Prescriptions
                .OrderBy(pr => pr.DueDate)
                .Select(pr => new PrescriptionResponse
                {
                    IdPrescription = pr.IdPrescription,
                    Date = pr.Date,
                    DueDate = pr.DueDate,
                    Doctor = new DoctorResponse
                    {
                        IdDoctor = pr.Doctor.IdDoctor,
                        FirstName = pr.Doctor.FirstName,
                        LastName = pr.Doctor.LastName
                    },
                    Medicaments = pr.PrescriptionMedicaments.Select(pm => new MedicamentResponse
                    {
                        IdMedicament = pm.Medicament.IdMedicament,
                        Name = pm.Medicament.Name,
                        Dose = pm.Dose,
                        Description = pm.Medicament.Description
                    }).ToList()
                }).ToList()
        };
    }
    
    public async Task<PatientResponse> GetPatientDetails(int idPatient)
    {
        var patient = await GetPatientFromDatabase(idPatient);

        if (patient == null)
        {
            throw new InvalidOperationException($"Patient with ID {idPatient} not found");
        }

        return MapPatientToResponse(patient);
    }
    
}
