using EndSickness.Application.Medicines.GetMedicineById.GetCreateMedicine;
using EndSickness.Shared.Medicines.Queries.GetMedicineById;

namespace EndSickness.Application.UnitTests.Tests.Medicines.Queries.GetMedicineById;

[Collection("QueryCollection")]
public class GetMedicineByIdQueryHandlerTest : QueryTestBase
{
    private readonly GetMedicineByIdQueryHandler _handler;
    public GetMedicineByIdQueryHandlerTest() : base()
    {
        _handler = new();
    }

    [Fact]
    public async Task GetMedicineByIdQueryTest()
    {
        var result = await _handler.Handle(new GetMedicineByIdQuery(1), CancellationToken.None);
        result.Id.Should().NotBe(0);
    }
}
