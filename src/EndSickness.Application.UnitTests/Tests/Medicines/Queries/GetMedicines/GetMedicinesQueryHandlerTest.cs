using EndSickness.Application.Medicines.GetMedicineById.GetMedicines;
using EndSickness.Shared.Medicines.Queries.GetMedicines;

namespace EndSickness.Application.UnitTests.Tests.Medicines.Queries.GetMedicines;

[Collection("QueryCollection")]
public class GetMedicinesQueryHandlerTest : QueryTestBase
{
    private readonly GetMedicinesQueryHandler _handler;
    public GetMedicinesQueryHandlerTest() : base()
    {
        _handler = new();
    }

    [Fact]
    public async Task GetMedicinesQueryTest()
    {
        var result = await _handler.Handle(new GetMedicinesQuery(), CancellationToken.None);
        result.Medicines.Count.Should().Be(2);
    }
}
