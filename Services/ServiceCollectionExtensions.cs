using Microsoft.EntityFrameworkCore;
using SiteSyncTracker.Data;

namespace SiteSyncTracker.Services;

public static class ServiceCollectionExtensions
{
    public static MauiAppBuilder AddSiteSyncServices(this MauiAppBuilder builder)
    {
        var databasePath = Path.Combine(FileSystem.AppDataDirectory, "sitesync.db");
        builder.Services.AddPooledDbContextFactory<AppDbContext>(options =>
            options.UseSqlite($"Data Source={databasePath}"));
        builder.Services.AddSingleton<InventoryService>();
        builder.Services.AddSingleton<RemoteSyncService>(serviceProvider =>
        {
            var baseUrl = Preferences.Default.Get("api_base_url", "https://localhost:7001/");
            var client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl, UriKind.Absolute),
                Timeout = TimeSpan.FromSeconds(30)
            };
            return new RemoteSyncService(client);
        });
        return builder;
    }
}
