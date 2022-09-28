using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CleanArchitecture.Blazor.Shared;

public partial class MainLayout
{
    bool _drawerOpen = true;

    async void DrawerToggle()
    {
        _drawerOpen = !_drawerOpen;
        await localStorage.SetItemAsync("drawer-open", _drawerOpen);
    }
    [Inject] public NavigationManager MyNavigationManager { get; set; } = default!;
    [Inject] public Blazored.LocalStorage.ILocalStorageService localStorage { get; set; }
    bool _isAdmin;
    bool _isDarkMode = false;
    private string ModeIcon => _isDarkMode ? Icons.Filled.DarkMode : Icons.Filled.LightMode;

    private async void ToggleDark()
    { 
        _isDarkMode = !_isDarkMode;
        await localStorage.SetItemAsync("dark-mode", _isDarkMode);
    }
   
    protected override async Task OnInitializedAsync()
    {
        _isAdmin = MyNavigationManager.ToBaseRelativePath(MyNavigationManager.Uri).StartsWith("admin");
        _isDarkMode = await localStorage.GetItemAsync<bool>("dark-mode");
        _drawerOpen = await localStorage.GetItemAsync<bool>("drawer-open");
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