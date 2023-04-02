using EndSickness.Application.UnitTests.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace EndSickness.Application.UnitTests.Tests
{
    public class MockTest : CommandTestBase
    {
        public MockTest() : base()
        {
            
        }

        [Fact]
        public async Task CheckDb()
        {
            var dir = await _context.AppUsers.FirstAsync(x => x.Id == 1, CancellationToken.None);
            dir.Should().NotBeNull();
        }
    }
}
