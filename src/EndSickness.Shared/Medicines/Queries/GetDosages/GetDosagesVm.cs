namespace EndSickness.Shared.Medicines.Queries.GetDosages;

public record GetDosagesVm
{
    public GetDosagesVm()
    {
        Dosages = new List<GetDosagesDto>();
    }
    public GetDosagesVm(ICollection<GetDosagesDto> medicines)
    {
        Dosages = medicines;
    }
    public ICollection<GetDosagesDto> Dosages { get; init; }
}