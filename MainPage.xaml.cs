using SiteSyncTracker.Services;

namespace SiteSyncTracker;

public partial class MainPage : ContentPage
{
    private readonly InventoryService _inventoryService;

    public MainPage()
    {
        InitializeComponent();
        _inventoryService = Application.Current?.Handler?.MauiContext?.Services.GetService<InventoryService>()
            ?? throw new InvalidOperationException("Inventory services are not initialized.");
    }

    private async void OnGoodsInClicked(object? sender, EventArgs e) => await Shell.Current.GoToAsync("GoodsInPage");

    private async void OnGoodsOutClicked(object? sender, EventArgs e) => await Shell.Current.GoToAsync("GoodsOutPage");

    private async void OnInventoryClicked(object? sender, EventArgs e) => await Shell.Current.GoToAsync("InventoryPage");
}
