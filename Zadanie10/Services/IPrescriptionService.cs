using Zadanie10.DTO_s;

namespace Zadanie10.Services;

public interface IPrescriptionService
{
    Task<Patient> GetOrCreatePatient(PatientRequest patientRequest);
    Task MedicamentsExist(IEnumerable<MedicamentRequest> medicaments);
    void ValidatePrescription(PrescriptionRequest request);
    Task<Prescription> AddPrescription(PrescriptionRequest request, int idDoctor);
    Task<Doctor> GetDoctorById(int idDoctor);
    Task<Patient> GetPatientFromDatabase(int idPatient);
    PatientResponse MapPatientToResponse(Patient patient);
    Task<PatientResponse> GetPatientDetails(int idPatient);
    
}