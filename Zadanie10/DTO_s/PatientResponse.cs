namespace Zadanie10.DTO_s;

public class PatientResponse
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }
    public List<PrescriptionResponse> Prescriptions { get; set; }
}
