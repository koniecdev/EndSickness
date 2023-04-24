using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Data;

public class MyPersistedGrantStore : IPersistedGrantStore
{
    private readonly ApplicationDbContext _dbContext;

    public MyPersistedGrantStore(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task StoreAsync(PersistedGrant grant)
    {
        var existingGrant = await _dbContext.PersistedGrants.FirstOrDefaultAsync(x => x.Key == grant.Key);

        if (existingGrant == null)
        {
            _dbContext.PersistedGrants.Add(grant);
        }
        else
        {
            existingGrant.ClientId = grant.ClientId;
            existingGrant.CreationTime = grant.CreationTime;
            existingGrant.Data = grant.Data;
            existingGrant.Expiration = grant.Expiration;
            existingGrant.SessionId = grant.SessionId;
            existingGrant.SubjectId = grant.SubjectId;
            existingGrant.Type = grant.Type;
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task<PersistedGrant> GetAsync(string key)
    {
        var grant = await _dbContext.PersistedGrants.FirstOrDefaultAsync(x => x.Key == key);

        return grant;
    }

    public async Task<IEnumerable<PersistedGrant>> GetAllAsync(string subjectId)
    {
        var grants = await _dbContext.PersistedGrants.Where(x => x.SubjectId == subjectId).ToArrayAsync();

        return grants;
    }

    public async Task RemoveAsync(string key)
    {
        var grant = await _dbContext.PersistedGrants.FirstOrDefaultAsync(x => x.Key == key);

        if (grant != null)
        {
            _dbContext.PersistedGrants.Remove(grant);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task RemoveAllAsync(string subjectId, string clientId)
    {
        var grants = await _dbContext.PersistedGrants.Where(x => x.SubjectId == subjectId && x.ClientId == clientId).ToListAsync();

        if (grants.Any())
        {
            _dbContext.PersistedGrants.RemoveRange(grants);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task RemoveAllAsync(string subjectId, string clientId, string type)
    {
        var grants = await _dbContext.PersistedGrants.Where(x => x.SubjectId == subjectId && x.ClientId == clientId && x.Type == type).ToListAsync();

        if (grants.Any())
        {
            _dbContext.PersistedGrants.RemoveRange(grants);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<PersistedGrant>> GetAllAsync(PersistedGrantFilter filter)
    {
        var persistedGrants = await _dbContext.PersistedGrants
            .Where(x => x.SubjectId == filter.SubjectId && x.ClientId == filter.ClientId)
            .ToListAsync();

        return persistedGrants;
    }

    public async Task RemoveAllAsync(PersistedGrantFilter filter)
    {
        var persistedGrants = await _dbContext.PersistedGrants
            .Where(x => x.SubjectId == filter.SubjectId && x.ClientId == filter.ClientId)
            .ToListAsync();

        _dbContext.PersistedGrants.RemoveRange(persistedGrants);
        await _dbContext.SaveChangesAsync();
    }
}