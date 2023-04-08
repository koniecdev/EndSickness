namespace EndSickness.Application.UnitTests.Mapping;

public class MappingTest : IClassFixture<MappingTestFixture>
{
    private readonly IConfigurationProvider _configurationProvider;

    public MappingTest(MappingTestFixture fixture)
    {
        _configurationProvider = fixture.ConfigurationProvider;
    }

    [Fact]
    public void ShouldHaveValidConfiguration()
    {
        _configurationProvider.AssertConfigurationIsValid();
    }
}
