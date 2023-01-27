using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimpleCalculator.Business;

namespace SimpleCalculator;

public partial class MainViewModel : ObservableObject
{
    private bool _isDark;
    
    public bool IsDark
    {
        get => _isDark;
        set
        {
            if (SetProperty(ref _isDark, value))
            {
                _ = AppThemeService.Instance.SetThemeAsync(value);
            }
        }
    }

    [ObservableProperty]
    private Calculator _calculator = new();

    [RelayCommand]
    private void Input(string key)
        => Calculator = Calculator.Input(key);
}
