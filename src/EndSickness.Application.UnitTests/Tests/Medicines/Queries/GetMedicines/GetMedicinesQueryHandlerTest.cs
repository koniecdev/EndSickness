using EndSickness.Application.Common.Exceptions;
using EndSickness.Application.Medicines.Queries.GetMedicines;
using EndSickness.Shared.Medicines.Queries.GetMedicines;

namespace EndSickness.Application.UnitTests.Tests.Medicines.Queries.GetMedicines;

[Collection("QueryCollection")]
public class GetMedicinesQueryHandlerTest : QueryTestBase
{
    private readonly GetMedicinesQueryHandler _handler;
    private readonly GetMedicinesQueryHandler _freshUserHandler;

    public GetMedicinesQueryHandlerTest() : base()
    {
        _handler = new(_context, _mapper, _currentUser);
        _freshUserHandler = new(_context, _mapper, _freshCurrentUser);
    }

    [Fact]
    public async Task GetMedicinesQueryTest_ShouldBeValid()
    {
        var result = await _handler.Handle(new GetMedicinesQuery(), CancellationToken.None);
        result.Medicines.Count.Should().Be(4);
    }

    [Fact]
    public async Task GetMedicinesQueryTest_ShouldBeEmpty()
    {
        try
        {
            var result = await _freshUserHandler.Handle(new GetMedicinesQuery(), CancellationToken.None);
            throw new Exception(SD.UnexpectedErrorInTestMethod);
        }
        catch(Exception ex)
        {
            ex.Should().BeOfType<EmptyResultException>();
        }
    }
}
