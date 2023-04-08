using EndSickness.Application.UnitTests.Common.CurrentUserServiceFactories;
using EndSickness.Shared.Common.Mappings;

namespace EndSickness.Application.UnitTests.Common;

public class QueryTestBase : IDisposable
{
    private bool _disposed = false;
    private readonly IDbContextMockFactory<EndSicknessContext> _dbContextMockFactory;
    private readonly ICurrentUserFactory _currentUserFactory;

    protected readonly EndSicknessContext _context;
    protected readonly IMapper _mapper;
    protected readonly ICurrentUserService _currentUser;
    protected readonly ICurrentUserService _unauthorizedCurrentUser;

    public QueryTestBase()
    {
        _dbContextMockFactory = new EndSicknessContextMockFactory();
        _context = _dbContextMockFactory.Create().Object;
        _mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); }).CreateMapper();

        _currentUserFactory = new ValidCurrentUserFactory();
        _currentUser = _currentUserFactory.Create();

        _currentUserFactory = new UnauthorizedCurrentUserFactory();
        _unauthorizedCurrentUser = _currentUserFactory.Create();
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