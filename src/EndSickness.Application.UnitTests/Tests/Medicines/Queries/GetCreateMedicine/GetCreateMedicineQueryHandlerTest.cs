using EndSickness.Application.Medicines.Queries.GetCreateMedicine;
using EndSickness.Shared.Medicines.Queries.GetCreateMedicine;

namespace EndSickness.Application.UnitTests.Tests.Medicines.Queries.GetCreateMedicine;

public class GetCreateMedicineQueryHandlerTest : QueryTestBase
{
    private readonly GetCreateMedicineQueryHandler _handler;
    public GetCreateMedicineQueryHandlerTest() : base()
    {
        _handler = new();
    }

    [Fact]
    public async Task GetCreateMedicineQueryTest()
    {
        var result = await _handler.Handle(new GetCreateMedicineQuery(), CancellationToken.None);
        result.Medicine.Should().NotBeNull();
    }
}
