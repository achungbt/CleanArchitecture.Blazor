using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CleanArchitecture.Blazor.Shared;

public partial class MainLayout
{
    bool _drawerOpen = true;

    async void DrawerToggle()
    {
        _drawerOpen = !_drawerOpen;
        await LocalStorage.SetItemAsync("drawer-open", _drawerOpen);
    }
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Inject] public Blazored.LocalStorage.ILocalStorageService LocalStorage { get; set; } = default!;
    [Inject] public AppState AppState  { get; set; } = default!;
    bool _isAdmin;
    private bool _isDarkMode;
    private string ModeIcon => !_isDarkMode ? Icons.Filled.DarkMode : Icons.Filled.LightMode;

    private async void ToggleDark()
    { 
        _isDarkMode = !_isDarkMode;
        
        await LocalStorage.SetItemAsync("dark-mode", _isDarkMode);
        AppState.SetThemMode(_isDarkMode);
    }
   
    protected override async Task OnInitializedAsync()
    {
        _isAdmin = NavigationManager.ToBaseRelativePath(NavigationManager.Uri).StartsWith("admin");
        _isDarkMode = await LocalStorage.GetItemAsync<bool>("dark-mode");
        _drawerOpen = await LocalStorage.GetItemAsync<bool>("drawer-open");
        if (_isDarkMode != AppState.IsDarkMode)
        {
            AppState.SetThemMode(_isDarkMode);
        }
    }
    
    readonly MudTheme _lightTheme = new MudTheme
    {
        Palette = new Palette()
        {
            AppbarBackground = "#ffffffcc",
            AppbarText = "#27272f",
        }
    };
}