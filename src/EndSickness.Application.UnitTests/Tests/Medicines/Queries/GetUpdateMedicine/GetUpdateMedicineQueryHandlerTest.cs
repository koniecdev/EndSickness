using EndSickness.Application.Medicines.Queries.GetUpdateMedicine;
using EndSickness.Shared.Medicines.Queries.GetUpdateMedicine;

namespace EndSickness.Application.UnitTests.Tests.Medicines.Queries.GetUpdateMedicine;

[Collection("QueryCollection")]
public class GetUpdateMedicineQueryHandlerTest : QueryTestBase
{
    private readonly GetUpdateMedicineQueryHandler _handler;
    private readonly GetUpdateMedicineQueryHandler _unauthorizedUser_handler;
    public GetUpdateMedicineQueryHandlerTest() : base()
    {
        _handler = new(_context, _mapper, _currentUser);
        _unauthorizedUser_handler = new(_context, _mapper, _unauthorizedCurrentUser);
    }

    [Fact]
    public async Task GetUpdateMedicineQueryTest_ValidRequest()
    {
        var result = await _handler.Handle(new GetUpdateMedicineQuery(1), CancellationToken.None);
        result.Medicine.Name.Should().Be("Nurofen");
    }

    [Fact]
    public async Task GetUpdateMedicineQueryTest_UnauthorizedUser()
    {
        try
        {
            var result = await _unauthorizedUser_handler.Handle(new GetUpdateMedicineQuery(1), CancellationToken.None);
            if(result != null)
            {
                throw new Exception("result of test which was suppose to throw error, did not threw any error. Check your mocks");
            }
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<UnauthorizedAccessException>();
        }
    }

    [Fact]
    public async Task GetUpdateMedicineQueryTest_InvalidId()
    {
        try
        {
            var result = await _handler.Handle(new GetUpdateMedicineQuery(int.MaxValue), CancellationToken.None);
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<InvalidOperationException>();
        }
    }
}
