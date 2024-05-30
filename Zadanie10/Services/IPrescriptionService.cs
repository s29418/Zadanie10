namespace Zadanie10.Services;

public interface IPrescriptionService
{
    Task<Patient> GetOrCreatePatient(Patient patient);
    Task<bool> MedicamentsExist(IEnumerable<int> medicamentIds);
    void ValidatePrescription(Prescription prescription);
    Task<Prescription> AddPrescription(Prescription prescription);
    Task<Doctor> GetDoctor(int id);
}