namespace EndSickness.Shared.Medicines.Queries.GetUpdateMedicine;

public class GetUpdateMedicineQuery : IRequest<GetUpdateMedicineVm>
{
    public GetUpdateMedicineQuery()
    {
        
    }
    public GetUpdateMedicineQuery(int id)
    {
        Id = id;
    }
    public int Id { get; set; }
}
