using EndSickness.Shared.Medicines.Queries.GetDosageById;

namespace EndSickness.Application.Medicines.Queries.GetDosageById;

public class GetDosageByIdQueryValidator : AbstractValidator<GetDosageByIdQuery>
{
    public GetDosageByIdQueryValidator()
    {
        RuleFor(m => m.MedicineId).NotEmpty().GreaterThan(0).LessThan(int.MaxValue);
    }
}
