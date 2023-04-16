using EndSickness.Application.Common.Exceptions;
using EndSickness.Application.MedicineLogs.Queries.GetMedicineLogs;
using EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogs;

namespace EndSickness.Application.UnitTests.Tests.MedicineLogs.Queries.GetMedicineLogs;

[Collection("QueryCollection")]
public class GetMedicineLogsQueryHandlerTest : QueryTestBase
{
    private readonly GetMedicineLogsQueryHandler _handler;
    private readonly GetMedicineLogsQueryHandler _freshUserHandler;

    public GetMedicineLogsQueryHandlerTest() : base()
    {
        _handler = new(_context, _mapper, _currentUser);
        _freshUserHandler = new(_context, _mapper, _freshCurrentUser);
    }

    [Fact]
    public async Task GetMedicinesQueryTest_ShouldBeValid()
    {
        var result = await _handler.Handle(new GetMedicineLogsQuery(), CancellationToken.None);
        result.MedicineLogs.Count.Should().Be(2);
    }
}
