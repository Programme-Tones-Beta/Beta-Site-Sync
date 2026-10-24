using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace SiteSyncTracker.Web.Tests;

public sealed class TestApplicationFactory : WebApplicationFactory<Program>
{
    private readonly string _databaseDirectory = Path.Combine(Path.GetTempPath(), "SiteSyncTrackerTests", Guid.NewGuid().ToString("N"));

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        Directory.CreateDirectory(_databaseDirectory);
        builder.UseEnvironment("Testing");
        builder.ConfigureAppConfiguration((_, configuration) =>
        {
            configuration.AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ConnectionStrings:Inventory"] = $"Data Source={Path.Combine(_databaseDirectory, "inventory.db")}",
                ["ConnectionStrings:Identity"] = $"Data Source={Path.Combine(_databaseDirectory, "identity.db")}",
                ["Admin:Email"] = "",
                ["Admin:Password"] = ""
            });
        });
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        try
        {
            if (Directory.Exists(_databaseDirectory)) Directory.Delete(_databaseDirectory, recursive: true);
        }
        catch (IOException)
        {
        }
    }
}
