using SiteSyncTracker.Models;
using SiteSyncTracker.Services;

namespace SiteSyncTracker.Views;

public partial class GoodsOutPage : ContentPage
{
    private readonly InventoryService _inventoryService;

    public GoodsOutPage()
    {
        InitializeComponent();
        _inventoryService = Application.Current?.Handler?.MauiContext?.Services.GetService<InventoryService>()
            ?? throw new InvalidOperationException("Inventory services are not initialized.");
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        LocationPicker.ItemsSource = (await _inventoryService.GetLocationsAsync()).ToList();
        StatusPicker.ItemsSource = (await _inventoryService.GetStatusesAsync()).ToList();
    }

    private async void OnConfirmIssueClicked(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ManifestEntry.Text) || !int.TryParse(QuantityEntry.Text, out var quantity)
            || quantity <= 0 || LocationPicker.SelectedItem is not Location location
            || StatusPicker.SelectedItem is not Status status)
        {
            await DisplayAlertAsync("Validation", "Enter a manifest, positive quantity, destination, and status.", "OK");
            return;
        }

        var item = (await _inventoryService.GetItemsAsync(ManifestEntry.Text.Trim()))
            .FirstOrDefault(inventoryItem => inventoryItem.ManifestNumber.Equals(ManifestEntry.Text.Trim(), StringComparison.OrdinalIgnoreCase));

        if (item is null)
        {
            await DisplayAlertAsync("Not found", "No inventory item matches that manifest.", "OK");
            return;
        }

        if (quantity > item.QuantityReceived)
        {
            await DisplayAlertAsync("Validation", "Quantity out cannot exceed quantity received.", "OK");
            return;
        }

        item.QuantityReceived -= quantity;
        item.CurrentLocationId = location.Id;
        item.CurrentLocation = location;
        item.CurrentStatusId = status.Id;
        item.CurrentStatus = status;
        await _inventoryService.UpdateAsync(item);

        ManifestEntry.Text = QuantityEntry.Text = string.Empty;
        await DisplayAlertAsync("Saved", "The material issue was recorded.", "OK");
    }
}
