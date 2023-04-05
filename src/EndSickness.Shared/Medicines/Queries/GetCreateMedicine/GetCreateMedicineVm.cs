namespace EndSickness.Shared.Medicines.Queries.GetCreateMedicine;

public class GetCreateMedicineVm
{
    public GetCreateMedicineVm()
    {
        Medicine = new();
    }
    public GetCreateMedicineMedicineDto Medicine { get; set; }
}