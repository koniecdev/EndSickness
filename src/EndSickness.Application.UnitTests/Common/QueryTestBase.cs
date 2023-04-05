using EndSickness.Shared.Common.Mappings;

namespace EndSickness.Application.UnitTests.Common;

public abstract class QueryTestBase : IDisposable
{
    private bool _disposed = false;
    private readonly IDbContextMockFactory<EndSicknessContext> _dbContextMockFactory;
    protected readonly EndSicknessContext _context;
    protected readonly IMapper _mapper;
    protected readonly ICurrentUserService _currentUser;

    public QueryTestBase()
    {
        _dbContextMockFactory = new EndSicknessContextMockFactory();
        _context = _dbContextMockFactory.Create().Object;
        _mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); }).CreateMapper();
        var currentUserMock = new Mock<ICurrentUserService>();
        currentUserMock.Setup(m => m.AppUserId).Returns("slayId");
        _currentUser = currentUserMock.Object;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // Dispose of any managed resources here
            }

            _dbContextMockFactory.Destroy(_context);
            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~QueryTestBase()
    {
        Dispose(false);
    }
}

[CollectionDefinition("QueryCollection")]
public class QueryCollection : ICollectionFixture<QueryTestBase>
{
    
}