namespace EndSickness.Shared.Medicines.Queries.GetUpdateMedicine;

public class GetUpdateMedicineVm
{
    public GetUpdateMedicineVm()
    {
        Medicine = new();
    }
    public GetUpdateMedicineMedicineDto Medicine { get; set; }
}