using EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogsByMedicineId;

namespace EndSickness.Application.MedicineLogs.Queries.GetMedicineLogsByMedicineId;

public class GetMedicineLogsByMedicineIdQueryValidator : AbstractValidator<GetMedicineLogsByMedicineIdQuery>
{
    public GetMedicineLogsByMedicineIdQueryValidator()
    {
        RuleFor(m => m.MedicineId).NotEmpty().GreaterThan(0).LessThan(int.MaxValue);
    }
}
