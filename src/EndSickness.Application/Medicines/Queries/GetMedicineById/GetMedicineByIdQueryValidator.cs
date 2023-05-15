using EndSickness.Shared.Medicines.Queries.GetMedicineById;

namespace EndSickness.Application.Medicines.Queries.GetMedicineById;

public class GetMedicineByIdQueryValidator : AbstractValidator<GetMedicineByIdQuery>
{
    public GetMedicineByIdQueryValidator()
    {
        RuleFor(m => m.Id).NotEmpty().GreaterThan(0).LessThan(int.MaxValue);
    }
}
