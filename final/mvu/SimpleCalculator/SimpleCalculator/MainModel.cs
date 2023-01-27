using SimpleCalculator.Business;
using Uno.Extensions.Reactive;
using Uno.Extensions.Toolkit;
using Windows.System;

namespace SimpleCalculator;

public partial record MainModel
{
    public IState<bool> IsDark { get; }

    public IState<Calculator> Calculator { get; }

    public async ValueTask InputCommand(string key, CancellationToken ct)
            => await Calculator.Update(c => c?.Input(key), ct);

    public MainModel()
    {
        Calculator = State.Value(this, () => new Calculator());
        IsDark = State.Value(this, () => AppThemeService.Instance.IsDark);

        IsDark.ForEachAsync(async (dark, ct) => 
            await AppThemeService.Instance.SetThemeAsync(dark, ct));
    }
}