using SiteSyncTracker.Models;
using SiteSyncTracker.Services;

namespace SiteSyncTracker.Views;

public partial class GoodsInPage : ContentPage
{
    private readonly InventoryService _inventoryService;

    public GoodsInPage()
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
        StatusPicker.SelectedIndex = 1;
    }

    private async void OnConfirmReceiptClicked(object? sender, EventArgs e)
    {
        if (!int.TryParse(ExpectedEntry.Text, out var expected) || !int.TryParse(ReceivedEntry.Text, out var received)
            || expected < 0 || received < 0 || string.IsNullOrWhiteSpace(ManifestEntry.Text)
            || string.IsNullOrWhiteSpace(DescriptionEntry.Text) || LocationPicker.SelectedItem is not Location location
            || StatusPicker.SelectedItem is not Status status)
        {
            await DisplayAlertAsync("Validation", "Enter valid manifest, description, quantities, location, and status values.", "OK");
            return;
        }

        await _inventoryService.AddAsync(new InventoryItem
        {
            ManifestNumber = ManifestEntry.Text.Trim(),
            Description = DescriptionEntry.Text.Trim(),
            QuantityExpected = expected,
            QuantityReceived = received,
            CurrentLocationId = location.Id,
            CurrentLocation = location,
            CurrentStatusId = status.Id,
            CurrentStatus = status,
            NotifyTo = NotifyEntry.Text?.Trim()
        });

        ManifestEntry.Text = DescriptionEntry.Text = ExpectedEntry.Text = ReceivedEntry.Text = NotifyEntry.Text = string.Empty;
        await DisplayAlertAsync("Saved", "The delivery was added to inventory.", "OK");
    }
}
