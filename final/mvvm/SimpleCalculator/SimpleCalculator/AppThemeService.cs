using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Uno.Toolkit.UI;
using Microsoft.UI.Xaml;

namespace SimpleCalculator;

public class AppThemeService
{
    private static AppThemeService? _instance;

    public static AppThemeService Instance => _instance ?? throw new Exception("You must call 'AppThemeService.Init(window)' prior to getting the current instance.");

    public static AppThemeService Init(Window window) =>
        new AppThemeService(window);

    private readonly Window _window;

    private AppThemeService(Window window)
    {
        _instance = this;
        _window = window;
    }

    public bool IsDark => SystemThemeHelper.IsRootInDarkMode(_window.Content.XamlRoot!);

    public async ValueTask SetThemeAsync(bool darkMode, CancellationToken ct = default)
    {
        var tcs = new TaskCompletionSource<object>();
        await using var _ = ct.Register(() => tcs.TrySetCanceled());
        _window.DispatcherQueue.TryEnqueue(() =>
        {
            if (!ct.IsCancellationRequested)
            {
                SystemThemeHelper.SetRootTheme(_window.Content.XamlRoot, darkMode);
            }
#nullable disable
            tcs.TrySetResult(default);
#nullable restore
        });
        await tcs.Task;
    }
}