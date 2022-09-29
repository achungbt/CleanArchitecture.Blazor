namespace CleanArchitecture.Blazor.Shared;

public class AppState
{
    public bool IsDarkMode { get; private set; }

    public event Action OnChange;

    public void SetThemMode(bool isDarkMode)
    {
        IsDarkMode = isDarkMode;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}