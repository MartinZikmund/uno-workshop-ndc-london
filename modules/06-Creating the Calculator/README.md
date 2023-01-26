# Creating the Calculator

Now that we have a basic understanding of the Uno Platform and how to build a simple app we can start building our Calculator. To start we will need to update the DataContext to use the Calculator engine included in the Workshop NuGet we installed earlier.

## Updating the DataContext

<details>
<summary>MVU-X</summary>

```cs
public partial record MainModel
{
    public IState<bool> IsDark { get; }

    public IState<Calculator> Calculator { get; }

    public async ValueTask Input(KeyInput key, CancellationToken ct) =>
        await Calculator.Update(c => c?.Input(key), ct);

    public async ValueTask InputVirtualKey(VirtualKey key, CancellationToken ct) =>
        await Calculator.Update(c => c?.Input(key), ct);

    public MainModel()
    {
        Calculator = State.Value(this, () => new Calculator());
        IsDark = State.Value(this, () => AppThemeService.Instance.IsDark)
        IsDark.ForEachAsync((dark, ct) => AppThemeService.Instance.SetThemeAsync(dark, ct));
    }
}
```
</details>

<details>
<summary>MVVM</summary>

```cs
public partial class MainViewModel : ObservableObject
{
    private bool _isDark = AppThemeService.Instance.IsDark;
    public bool IsDark
    {
        get => _isDark;
        set => SetProperty(ref _isDark, value, async dark => await AppThemeService.Instance.SetThemeAsync(dark, default));
    }

    [ObservableProperty]
    private Calculator _calculator = new Calculator();

    [RelayCommand]
    private void Input(KeyInput key) => Calculator.Input(key);

    [RelayCommand]
    private void InputVirtualKey(VirtualKey key) => Calculator.Input(key);
}
```
</details>

## Creating the Calculator UI

> **NOTE:** When Binding the Commands note that the MVU-X commands will be `Input` and `InputVirtualKey` while the MVVM commands will be generated as `InputCommand` and `InputVirtualKeyCommand`. The code samples below will use the MVU style `Input` & `InputVirtualKey`. If you are using MVVM be sure to update the command names for the Bindings.

<details>
<summary>XAML</summary>

To Start let's update the section of our UI where we have 2 existing TextBlocks one currently with the Text `Equation` and the other bound to `Ouput` from the last module. Here we will create a Binding for the Calculator properties for the Equation and the Output.

```xml
<TextBlock Text="{Binding Calculator.Equation}"
           utu:AutoLayout.CounterAlignment="End"
           Foreground="{ThemeResource OnSecondaryContainerBrush}"
           Style="{StaticResource DisplaySmall}" />
<TextBlock Text="{Binding Calculator.Output}"
           utu:AutoLayout.CounterAlignment="End"
           Foreground="{ThemeResource OnBackgroundBrush}"
           Style="{StaticResource DisplayLarge}" />
```

Before we can add the buttons for our Calculator we need to add an XML Namespace to be able to pass the specific Enum value for each of our Keys in the Command Parameter.

```xml
<Page x:Class="SimpleCalculator.MainPage"
      xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
      xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
      xmlns:utu="using:Uno.Toolkit.UI"
      xmlns:um="using:Uno.Material"
      xmlns:calc="using:SimpleCalculator.Business"
      Background="{ThemeResource BackgroundBrush}">
```

Now we can add the buttons for our Calculator. For this we will need to replace the TextBox and Button with the following content:

```xml
<utu:AutoLayout Spacing="16" Orientation="Horizontal" Height="72">
  <Button Content="C"
          Command="{Binding Input}"
          CommandParameter="{x:Bind calc:KeyInput.Clear}"
          FontSize="32"
          Background="{ThemeResource PrimaryContainerBrush}"
          utu:AutoLayout.PrimaryAlignment="Stretch"
          Foreground="{ThemeResource OnSecondaryContainerBrush}" />
  <Button Content="±"
          Command="{Binding Input}"
          CommandParameter="{x:Bind calc:KeyInput.PlusMinus}"
          FontSize="32"
          Background="{ThemeResource PrimaryContainerBrush}"
          utu:AutoLayout.PrimaryAlignment="Stretch"
          Foreground="{ThemeResource OnSecondaryContainerBrush}" />
  <Button Content="%"
          Command="{Binding Input}"
          CommandParameter="{x:Bind calc:KeyInput.Percentage}"
          FontSize="32"
          Background="{ThemeResource PrimaryContainerBrush}"
          utu:AutoLayout.PrimaryAlignment="Stretch"
          Foreground="{ThemeResource OnSecondaryContainerBrush}" />
  <Button Content="÷"
          Command="{Binding Input}"
          CommandParameter="{x:Bind calc:KeyInput.Division}"
          FontSize="32"
          Background="{ThemeResource PrimaryVariantDarkBrush}"
          utu:AutoLayout.PrimaryAlignment="Stretch"
          Foreground="{ThemeResource OnTertiaryBrush}" />
</utu:AutoLayout>
<utu:AutoLayout Spacing="16" Orientation="Horizontal" Height="72">
  <Button Content="7"
          Command="{Binding Input}"
          CommandParameter="{x:Bind calc:KeyInput.Seven}"
          FontSize="32"
          Background="{ThemeResource SecondaryContainerBrush}"
          utu:AutoLayout.PrimaryAlignment="Stretch"
          Foreground="{ThemeResource OnSurfaceBrush}" />
  <Button Content="8"
          Command="{Binding Input}"
          CommandParameter="{x:Bind calc:KeyInput.Eight}"
          FontSize="32"
          Background="{ThemeResource SecondaryContainerBrush}"
          utu:AutoLayout.PrimaryAlignment="Stretch"
          Foreground="{ThemeResource OnSurfaceBrush}" />
  <Button Content="9"
          Command="{Binding Input}"
          CommandParameter="{x:Bind calc:KeyInput.Nine}"
          FontSize="32"
          Background="{ThemeResource SecondaryContainerBrush}"
          utu:AutoLayout.PrimaryAlignment="Stretch"
          Foreground="{ThemeResource OnSurfaceBrush}" />
  <Button Content="×"
          Command="{Binding Input}"
          CommandParameter="{x:Bind calc:KeyInput.Multiplication}"
          FontSize="32"
          Background="{ThemeResource PrimaryVariantDarkBrush}"
          utu:AutoLayout.PrimaryAlignment="Stretch"
          Foreground="{ThemeResource OnTertiaryBrush}" />
</utu:AutoLayout>
<utu:AutoLayout Spacing="16" Orientation="Horizontal" Height="72">
  <Button Content="4"
          Command="{Binding Input}"
          CommandParameter="{x:Bind calc:KeyInput.Four}"
          FontSize="32"
          Background="{ThemeResource SecondaryContainerBrush}"
          utu:AutoLayout.PrimaryAlignment="Stretch"
          Foreground="{ThemeResource OnSurfaceBrush}" />
  <Button Content="5"
          Command="{Binding Input}"
          CommandParameter="{x:Bind calc:KeyInput.Five}"
          FontSize="32"
          Background="{ThemeResource SecondaryContainerBrush}"
          utu:AutoLayout.PrimaryAlignment="Stretch"
          Foreground="{ThemeResource OnSurfaceBrush}" />
  <Button Content="6"
          Command="{Binding Input}"
          CommandParameter="{x:Bind calc:KeyInput.Six}"
          FontSize="32"
          Background="{ThemeResource SecondaryContainerBrush}"
          utu:AutoLayout.PrimaryAlignment="Stretch"
          Foreground="{ThemeResource OnSurfaceBrush}" />
  <Button Content="−"
          Command="{Binding Input}"
          CommandParameter="{x:Bind calc:KeyInput.Subtraction}"
          FontSize="32"
          Background="{ThemeResource PrimaryVariantDarkBrush}"
          utu:AutoLayout.PrimaryAlignment="Stretch"
          Foreground="{ThemeResource OnTertiaryBrush}" />
</utu:AutoLayout>
<utu:AutoLayout Spacing="16" Orientation="Horizontal" Height="72">
  <Button Content="1"
          Command="{Binding Input}"
          CommandParameter="{x:Bind calc:KeyInput.One}"
          FontSize="32"
          Background="{ThemeResource SecondaryContainerBrush}"
          utu:AutoLayout.PrimaryAlignment="Stretch"
          Foreground="{ThemeResource OnSurfaceBrush}" />
  <Button Content="2"
          Command="{Binding Input}"
          CommandParameter="{x:Bind calc:KeyInput.Two}"
          FontSize="32"
          Background="{ThemeResource SecondaryContainerBrush}"
          utu:AutoLayout.PrimaryAlignment="Stretch"
          Foreground="{ThemeResource OnSurfaceBrush}" />
  <Button Content="3"
          Command="{Binding Input}"
          FontSize="32"
          CommandParameter="{x:Bind calc:KeyInput.Three}"
          Background="{ThemeResource SecondaryContainerBrush}"
          utu:AutoLayout.PrimaryAlignment="Stretch"
          Foreground="{ThemeResource OnSurfaceBrush}" />
  <Button Content="+"
          Command="{Binding Input}"
          FontSize="32"
          CommandParameter="{x:Bind calc:KeyInput.Addition}"
          Background="{ThemeResource PrimaryVariantDarkBrush}"
          utu:AutoLayout.PrimaryAlignment="Stretch"
          Foreground="{ThemeResource OnTertiaryBrush}" />
</utu:AutoLayout>
<utu:AutoLayout Spacing="16" Orientation="Horizontal" Height="72">
  <Button Content="."
          Command="{Binding Input}"
          CommandParameter="{x:Bind calc:KeyInput.Dot}"
          FontSize="32"
          Background="{ThemeResource SecondaryContainerBrush}"
          utu:AutoLayout.PrimaryAlignment="Stretch"
          Foreground="{ThemeResource OnSurfaceBrush}" />
  <Button Content="0"
          Command="{Binding Input}"
          CommandParameter="{x:Bind calc:KeyInput.Zero}"
          FontSize="32"
          Background="{ThemeResource SecondaryContainerBrush}"
          utu:AutoLayout.PrimaryAlignment="Stretch"
          Foreground="{ThemeResource OnSurfaceBrush}" />
  <Button Content="&lt;-"
          Command="{Binding Input}"
          CommandParameter="{x:Bind calc:KeyInput.Back}"
          FontSize="32"
          Background="{ThemeResource SecondaryContainerBrush}"
          utu:AutoLayout.PrimaryAlignment="Stretch"
          Foreground="{ThemeResource OnSurfaceBrush}" />
  <Button Content="="
          Command="{Binding Input}"
          CommandParameter="{x:Bind calc:KeyInput.Equal}"
          FontSize="32"
          Background="{ThemeResource PrimaryVariantDarkBrush}"
          utu:AutoLayout.PrimaryAlignment="Stretch"
          Foreground="{ThemeResource OnTertiaryBrush}" />
</utu:AutoLayout>
```

Now we can run the Calculator with a fully functional UI running the Calculator engine. If you run on a desktop target such as Windows you will notice however that if you press common buttons like one of the numbers on your keyboard that nothing happens. For this we will need to add the KeyboardBehavior.

```xml
<Page x:Class="SimpleCalculator.MainPage"
      xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
      xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
      xmlns:utu="using:Uno.Toolkit.UI"
      xmlns:um="using:Uno.Material"
      xmlns:calc="using:SimpleCalculator.Business"
      xmlns:keyboard="using:SimpleCalculator.Keyboard"
      Background="{ThemeResource BackgroundBrush}"
      keyboard:KeyboardBehavior.KeyUpCommand="{Binding InputVirtualKey}">
```

Now we can run this on a desktop target and press common keys such as 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, +, -, *, /, =, ., and Backspace to interact with the Calculator.
</details>

<details>
<summary>C# Markup</summary>

To Start let's update the section of our UI where we have 2 existing TextBlocks one currently with the Text `Equation` and the other bound to `Ouput` from the last module. Here we will create a Binding for the Calculator properties for the Equation and the Output.

```cs
new TextBlock()
    .Text(() => dataContext.Calculator.Equation)
    .AutoLayout(counterAlignment: AutoLayoutAlignment.End)
    .Foreground(Theme.Brushes.OnSecondary.Container.Default)
    .Style(Theme.Styles.TextBlock.DisplaySmall),
new TextBlock()
    .Text(() => dataContext.Calculator.Output)
    .AutoLayout(counterAlignment: AutoLayoutAlignment.End)
    .Foreground(Theme.Brushes.OnBackground.Default)
    .Style(Theme.Styles.TextBlock.DisplayLarge)
```

Now we can add the buttons for the Calculator. Because the buttons will be largely repetitive we will go ahead and create a method to help simplify this.

<details>
<summary>MVU-X</summary>

```cs
private static AutoLayout KeyPadRow(params FrameworkElement[] children) =>
    new AutoLayout()
        .Spacing(16)
        .Orientation(Horizontal)
        .Height(72)
        .Children(children);

private static Button KeyPadButton(BindableMainModel dataContext, string content, KeyInput parameter, Action<IDependencyPropertyBuilder<Brush>> background = null, Action<IDependencyPropertyBuilder<Brush>> foreground = null) =>
    new Button()
        .Content(content)
        .Command(() => dataContext.Input)
        .CommandParameter(parameter)
        .FontSize(32)
        .Background(background ?? Theme.Brushes.Secondary.Container.Default)
        .Foreground(foreground ?? Theme.Brushes.OnSurface.Default)
        .AutoLayout(primaryAlignment: AutoLayoutAlignment.Stretch);
```
</details>

<details>
<summary>MVVM</summary>

```cs
private static AutoLayout KeyPadRow(params FrameworkElement[] children) =>
    new AutoLayout()
        .Spacing(16)
        .Orientation(Horizontal)
        .Height(72)
        .Children(children);

private static Button KeyPadButton(MainViewModel dataContext, string content, KeyInput parameter, Action<IDependencyPropertyBuilder<Brush>> background = null, Action<IDependencyPropertyBuilder<Brush>> foreground = null) =>
    new Button()
        .Content(content)
        .Command(() => dataContext.InputCommand)
        .CommandParameter(parameter)
        .FontSize(32)
        .Background(background ?? Theme.Brushes.Secondary.Container.Default)
        .Foreground(foreground ?? Theme.Brushes.OnSurface.Default)
        .AutoLayout(primaryAlignment: AutoLayoutAlignment.Stretch);
```
</details>

In order to add the buttons we will need to replace the TextBox and Button with the following content:

```cs
KeyPadRow(
    KeyPadButton(dataContext, "C", KeyInput.Clear),
    KeyPadButton(dataContext, "±", KeyInput.PlusMinus),
    KeyPadButton(dataContext, "%", KeyInput.Percentage),
    KeyPadButton(dataContext, "÷", KeyInput.Division)
),
KeyPadRow(
    KeyPadButton(dataContext, "7", KeyInput.Seven),
    KeyPadButton(dataContext, "8", KeyInput.Eight),
    KeyPadButton(dataContext, "9", KeyInput.Nine),
    KeyPadButton(dataContext, "×", KeyInput.Multiplication)
),
KeyPadRow(
    KeyPadButton(dataContext, "4", KeyInput.Four),
    KeyPadButton(dataContext, "5", KeyInput.Five),
    KeyPadButton(dataContext, "6", KeyInput.Six),
    KeyPadButton(dataContext, "−", KeyInput.Subtraction)
),
KeyPadRow(
    KeyPadButton(dataContext, "1", KeyInput.One),
    KeyPadButton(dataContext, "2", KeyInput.Two),
    KeyPadButton(dataContext, "3", KeyInput.Three),
    KeyPadButton(dataContext, "+", KeyInput.Addition)
),
KeyPadRow(
    KeyPadButton(dataContext, ".", KeyInput.Dot),
    KeyPadButton(dataContext, "0", KeyInput.Zero),
    KeyPadButton(dataContext, "<-", KeyInput.Back),
    KeyPadButton(dataContext, "=", KeyInput.Equal)
),
```

// TODO: We either need the generator for the Attached KeyboardBehavior property or we need it moved somewhere we can rely on a generated extension to attach to the page

</details>