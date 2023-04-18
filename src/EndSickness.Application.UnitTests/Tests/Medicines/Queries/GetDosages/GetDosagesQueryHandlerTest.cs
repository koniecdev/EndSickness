using EndSickness.Application.Medicines.Queries.GetDosages;

namespace EndSickness.Application.UnitTests.Tests.Dosages.Queries.GetDosages;

[Collection("QueryCollection")]
public class GetDosagesQueryHandlerTest : QueryTestBase
{
    private readonly GetDosagesQueryHandler _handler;
    private readonly GetDosagesQueryHandler _freshUserHandler;

    public GetDosagesQueryHandlerTest() : base()
    {
        _handler = new(_context, _currentUser);
        _freshUserHandler = new(_context, _freshCurrentUser);
    }

    
}
