using EndSickness.Application.Medicines.Queries.GetMedicines;
using EndSickness.Shared.Medicines.Queries.GetMedicines;

namespace EndSickness.Application.UnitTests.Tests.Medicines.Queries.GetMedicines;

[Collection("QueryCollection")]
public class GetMedicinesQueryHandlerTest : QueryTestBase
{
    private readonly GetMedicinesQueryHandler _handler;
    //private readonly GetMedicinesQueryHandler _notAuthorizedHandler;
    private readonly GetMedicinesQueryHandler _freshUserHandler;

    public GetMedicinesQueryHandlerTest() : base()
    {
        _handler = new(_context, _mapper, _currentUser);
        //_notAuthorizedHandler = new(_context, _mapper, _unauthorizedCurrentUser);
        _freshUserHandler = new(_context, _mapper, _freshCurrentUser);
    }

    [Fact]
    public async Task GetMedicinesQueryTest_ShouldBeValid()
    {
        var result = await _handler.Handle(new GetMedicinesQuery(), CancellationToken.None);
        result.Medicines.Count.Should().Be(2);
    }

    [Fact]
    public async Task GetMedicinesQueryTest_ShouldAllProperiesBePopulated()
    {
        var resultOuter = await _handler.Handle(new GetMedicinesQuery(), CancellationToken.None);
        var result = resultOuter.Medicines.First(m=>m.Id == 1);
        (result.Id == 1 && result.Name == "Nurofen" && result.Cooldown.Equals(TimeSpan.FromHours(4))
            && result.AppUserId == 1337 && result.MaxDailyAmount == 3
            && result.MaxDaysOfTreatment.Equals(TimeSpan.FromDays(7)))
            .Should().Be(true);
    }

    [Fact]
    public async Task GetMedicinesQueryTest_ShouldBeEmpty()
    {
        var result = await _freshUserHandler.Handle(new GetMedicinesQuery(), CancellationToken.None);
        result.Medicines.Count.Should().Be(0);
    }
}
