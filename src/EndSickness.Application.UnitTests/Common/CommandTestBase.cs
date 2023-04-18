using EndSickness.Application.UnitTests.Common.CurrentUserServiceFactories;
using EndSickness.Shared.Common.Mappings;

namespace EndSickness.Application.UnitTests.Common;

public class CommandTestBase : IDisposable
{
    private bool _disposed = false;
    private readonly IDbContextMockFactory<EndSicknessContext> _dbContextMockFactory;
    private readonly ICurrentUserFactory _currentUserFactory;
    private readonly IResourceOwnershipFactory _resourceOwnershipFactory;

    protected readonly EndSicknessContext _context;
    protected readonly IMapper _mapper;
    protected readonly IDateTime _time;
    protected readonly ICurrentUserService _currentUser;
    protected readonly ICurrentUserService _unauthorizedCurrentUser;
    protected readonly ICurrentUserService _forbiddenCurrentUser;

    protected readonly IResourceOwnershipService _resourceOwnershipValidUser;
    protected readonly IResourceOwnershipService _resourceOwnershipUnauthorizedUser;
    protected readonly IResourceOwnershipService _resourceOwnershipForbiddenUser;



    public CommandTestBase()
    {
        _dbContextMockFactory = new EndSicknessContextMockFactory();
        _context = _dbContextMockFactory.Create().Object;
        _mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); }).CreateMapper();

        var dateTimeMock = new Mock<IDateTime>();
        dateTimeMock.Setup(m => m.Now).Returns(new DateTime(2023, 12, 4, 4, 4, 4));
        _time = dateTimeMock.Object;

        _currentUserFactory = new ValidCurrentUserFactory();
        _currentUser = _currentUserFactory.Create();

        _currentUserFactory = new UnauthorizedCurrentUserFactory();
        _unauthorizedCurrentUser = _currentUserFactory.Create();

        _currentUserFactory = new FreshCurrentUserFactory();
        _forbiddenCurrentUser = _currentUserFactory.Create();

        _resourceOwnershipFactory = new ResourceOwnershipServiceValidUserFactory();
        _resourceOwnershipValidUser = _resourceOwnershipFactory.Create();

        _resourceOwnershipFactory = new ResourceOwnershipServiceUnauthorizedFactory();
        _resourceOwnershipUnauthorizedUser = _resourceOwnershipFactory.Create();

        _resourceOwnershipFactory = new ResourceOwnershipServiceInvalidUserFactory();
        _resourceOwnershipForbiddenUser = _resourceOwnershipFactory.Create();
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

    ~CommandTestBase()
    {
        Dispose(false);
    }
}