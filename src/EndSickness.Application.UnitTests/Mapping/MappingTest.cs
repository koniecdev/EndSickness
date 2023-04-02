namespace EndSickness.Application.UnitTests.Mapping;

public class MappingTest : IClassFixture<MappingTestFixture>
{
    private readonly IConfigurationProvider _configurationProvider;
    //private readonly IMapper _mapper;

    public MappingTest(MappingTestFixture fixture)
    {
        _configurationProvider = fixture.ConfigurationProvider;
        //_mapper = fixture.Mapper;
    }

    [Fact]
    public void ShouldHaveValidConfiguration()
    {
        _configurationProvider.AssertConfigurationIsValid();
    }
}
