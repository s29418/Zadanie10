namespace Zadanie10.DTO_s;

public class PrescriptionResponse
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public List<MedicamentResponse> Medicaments { get; set; }
    public DoctorResponse Doctor { get; set; }
}