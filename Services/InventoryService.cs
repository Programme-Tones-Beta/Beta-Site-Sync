using Microsoft.EntityFrameworkCore;
using SiteSyncTracker.Data;
using SiteSyncTracker.Models;

namespace SiteSyncTracker.Services;

public sealed class InventoryService
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public InventoryService(IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        await using var db = await _contextFactory.CreateDbContextAsync(cancellationToken);
        await db.Database.EnsureCreatedAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<InventoryItem>> GetItemsAsync(string? search = null, CancellationToken cancellationToken = default)
    {
        await using var db = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var query = db.InventoryItems
            .AsNoTracking()
            .Include(item => item.CurrentStatus)
            .Include(item => item.CurrentLocation)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();
            query = query.Where(item => item.ManifestNumber.Contains(search) || item.Description.Contains(search));
        }

        return await query.OrderBy(item => item.ManifestNumber).ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Status>> GetStatusesAsync(CancellationToken cancellationToken = default)
    {
        await using var db = await _contextFactory.CreateDbContextAsync(cancellationToken);
        return await db.Statuses.AsNoTracking().OrderBy(status => status.Id).ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Location>> GetLocationsAsync(CancellationToken cancellationToken = default)
    {
        await using var db = await _contextFactory.CreateDbContextAsync(cancellationToken);
        return await db.Locations.AsNoTracking().OrderBy(location => location.Id).ToListAsync(cancellationToken);
    }

    public async Task AddAsync(InventoryItem item, CancellationToken cancellationToken = default)
    {
        await using var db = await _contextFactory.CreateDbContextAsync(cancellationToken);
        item.Id = 0;
        item.DateUpdated = DateTime.UtcNow;
        db.InventoryItems.Add(item);
        await db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(InventoryItem item, CancellationToken cancellationToken = default)
    {
        await using var db = await _contextFactory.CreateDbContextAsync(cancellationToken);
        item.DateUpdated = DateTime.UtcNow;
        db.InventoryItems.Update(item);
        await db.SaveChangesAsync(cancellationToken);
    }
}
