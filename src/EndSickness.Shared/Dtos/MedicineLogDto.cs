namespace EndSickness.Shared.Dtos;

public record MedicineLogDto : IMapQuery<MedicineLog>
{
    public MedicineLogDto()
    {
    }
    public MedicineLogDto(int id, DateTime lastlyTaken, MedicineDto medicine)
    {
        Id = id;
        LastlyTaken = lastlyTaken;
        Medicine = medicine;
    }
    public int Id { get; init; }
    public DateTime LastlyTaken { get; init; }
    public MedicineDto Medicine { get; init; } = null!;
}