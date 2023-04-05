using EndSickness.Application.Medicines.Queries.GetUpdateMedicine;
using EndSickness.Shared.Medicines.Queries.GetUpdateMedicine;

namespace EndSickness.Application.UnitTests.Tests.Medicines.Queries.GetUpdateMedicine;

public class GetUpdateMedicineQueryHandlerTest : QueryTestBase
{
    private readonly GetUpdateMedicineQueryHandler _handler;
    public GetUpdateMedicineQueryHandlerTest() : base()
    {
        _handler = new(_context, _mapper, _currentUser);
    }

    [Fact]
    public async Task GetUpdateMedicineQueryTest()
    {
        var result = await _handler.Handle(new GetUpdateMedicineQuery(1), CancellationToken.None);
        result.Medicine.Name.Should().Be("Nurofen");
    }
}
