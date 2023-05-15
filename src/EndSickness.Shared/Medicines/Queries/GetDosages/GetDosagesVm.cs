using EndSickness.Shared.Dtos;

namespace EndSickness.Shared.Medicines.Queries.GetDosages;

public record GetDosagesVm(ICollection<DosageDto> Dosages);