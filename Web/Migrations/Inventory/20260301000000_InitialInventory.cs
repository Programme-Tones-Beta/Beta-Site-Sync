using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using SiteSyncTracker.Data;

#nullable disable

namespace SiteSyncTracker.Web.Migrations.Inventory;

[DbContext(typeof(AppDbContext))]
[Migration("20260301000000_InitialInventory")]
public partial class InitialInventory : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Locations",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false).Annotation("Sqlite:Autoincrement", true),
                ZoneName = table.Column<string>(maxLength: 100, nullable: false),
                GPSCoordinates = table.Column<string>(maxLength: 50, nullable: true),
                NotifyTo = table.Column<string>(maxLength: 255, nullable: true)
            }, constraints: table => table.PrimaryKey("PK_Locations", x => x.Id));

        migrationBuilder.CreateTable(
            name: "Statuses",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false).Annotation("Sqlite:Autoincrement", true),
                StatusName = table.Column<string>(maxLength: 50, nullable: false),
                NotifyTo = table.Column<string>(maxLength: 255, nullable: true)
            }, constraints: table => table.PrimaryKey("PK_Statuses", x => x.Id));

        migrationBuilder.CreateTable(
            name: "InventoryItems",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false).Annotation("Sqlite:Autoincrement", true),
                ManifestNumber = table.Column<string>(maxLength: 50, nullable: false),
                Description = table.Column<string>(maxLength: 255, nullable: false),
                QuantityExpected = table.Column<int>(nullable: false),
                QuantityReceived = table.Column<int>(nullable: false),
                DateUpdated = table.Column<DateTime>(nullable: false),
                CurrentStatusId = table.Column<int>(nullable: false),
                CurrentLocationId = table.Column<int>(nullable: false),
                NotifyTo = table.Column<string>(maxLength: 255, nullable: true)
            }, constraints: table =>
            {
                table.PrimaryKey("PK_InventoryItems", x => x.Id);
                table.ForeignKey("FK_InventoryItems_Locations_CurrentLocationId", x => x.CurrentLocationId, "Locations", "Id", onDelete: ReferentialAction.Cascade);
                table.ForeignKey("FK_InventoryItems_Statuses_CurrentStatusId", x => x.CurrentStatusId, "Statuses", "Id", onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.Sql("INSERT INTO Statuses (Id, StatusName) VALUES (1, 'In Transit'), (2, 'Delivered to Yard'), (3, 'Staged on Level'), (4, 'Installed');");
        migrationBuilder.Sql("INSERT INTO Locations (Id, ZoneName) VALUES (1, 'Main Laydown Yard'), (2, 'Level 1 Structure');");
        migrationBuilder.CreateIndex("IX_InventoryItems_CurrentLocationId", "InventoryItems", "CurrentLocationId");
        migrationBuilder.CreateIndex("IX_InventoryItems_CurrentStatusId", "InventoryItems", "CurrentStatusId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable("InventoryItems");
        migrationBuilder.DropTable("Locations");
        migrationBuilder.DropTable("Statuses");
    }
}
