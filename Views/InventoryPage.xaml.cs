using SiteSyncTracker.Services;

namespace SiteSyncTracker.Views;

public partial class InventoryPage : ContentPage
{
    private readonly InventoryService _inventoryService;

    public InventoryPage()
    {
        InitializeComponent();
        _inventoryService = Application.Current?.Handler?.MauiContext?.Services.GetService<InventoryService>()
            ?? throw new InvalidOperationException("Inventory services are not initialized.");
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadItemsAsync();
    }

    private async void OnSearchTextChanged(object? sender, TextChangedEventArgs e)
    {
        await LoadItemsAsync(e.NewTextValue);
    }

    private async Task LoadItemsAsync(string? search = null)
    {
        LoadingIndicator.IsVisible = true;
        try
        {
            InventoryList.ItemsSource = await _inventoryService.GetItemsAsync(search);
        }
        catch (Exception exception)
        {
            await DisplayAlertAsync("Inventory", exception.Message, "OK");
        }
        finally
        {
            LoadingIndicator.IsVisible = false;
        }
    }
}
