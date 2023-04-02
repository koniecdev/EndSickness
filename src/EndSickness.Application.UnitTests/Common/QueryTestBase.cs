﻿namespace EndSickness.Application.UnitTests.Common;

public abstract class QueryTestBase : IDisposable
{
    private bool disposed = false;
    private readonly IDbContextMockFactory<EndSicknessContext> _dbContextMockFactory;
    protected readonly EndSicknessContext _context;
    protected readonly IMapper _mapper;

    public QueryTestBase()
    {
        _dbContextMockFactory = new EndSicknessContextMockFactory();
        _context = _dbContextMockFactory.Create().Object;
        _mapper = new MapperConfiguration(cfg => { }).CreateMapper();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                // Dispose of any managed resources here
            }

            _dbContextMockFactory.Destroy(_context);
            disposed = true;
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