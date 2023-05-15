using EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogById;

namespace EndSickness.Application.MedicineLogs.Queries.GetMedicineLogById;

public class GetMedicineLogByIdQueryValidator : AbstractValidator<GetMedicineLogByIdQuery>
{
    public GetMedicineLogByIdQueryValidator()
    {
        RuleFor(m => m.Id).NotEmpty().GreaterThan(0).LessThan(int.MaxValue);
    }
}
