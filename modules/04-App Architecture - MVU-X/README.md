# Model View Update for Xaml (MVU-X)

Model View Update has become an increasingly popular choice for developers looking to have a more functional approach to building user interfaces. It is a pattern that is well suited to the Uno Platform, and is a great way to build your apps. In this module we will be looking at how to use the MVU-X pattern to build our UI. MVU-X is a pattern that is based on the MVU pattern, but is designed to work with XAML using Uno.Extensions.Reactive. In order to begin building our first Model we will need to install Uno.Extensions.Reactive.

<picture>
  <source media="(prefers-color-scheme: dark)" srcset="../../art/Dark/UnoExtensionsReactive.png">
  <source media="(prefers-color-scheme: light)" srcset="../../art/Light/UnoExtensionsReactive.png">
  <img alt="Install Uno.Toolkit.WinUI" src="../../art/Light/UnoExtensionsReactive.png">
</picture>

## Getting Started

To start off we will need to create a new model called `MainModel` and add a couple of properties to it. We'll add two IState properties that we will then bind to in our View.

```cs
public partial record MainModel
{
    public MainModel()
    {
        IsDark = State<bool>.Value(this, () => AppThemeService.Instance.IsDark);
        Output = State<string>.Value(this, () => string.Empty);
    }

    public IState<bool> IsDark { get; }

    public IState<string> Output { get; }
}
```

Now that we have our model created we will have two properties we can work with. But we still need some way to handle when the IsDark state is toggled. To do this we will need to add one more line to our constructor after we initialize the property.

```cs
public MainModel()
{
    IsDark = State<bool>.Value(this, () => AppThemeService.Instance.IsDark);
    Output = State<string>.Value(this, () => string.Empty);

    IsDark.ForEachAsync((dark, ct) => AppThemeService.Instance.SetThemeAsync(dark, ct));
}
```

## Binding to properties in the UI

With our model created we will now need to set up our DataContext and create some bindings.

<details>
<summary>XAML</summary>

To start let's set the DataContext in the MainPage.xaml.cs (code behind).

```cs
public partial class MainPage : Page
{
    public MainPage()
    {
        InitializeComponent();
        DataContext = new BindableMainModel();
    }
}
```

You'll notice that we didn't create the an instance of our MainModel but rather we created a `BindableMainModel`. This is the `X` in `MVU-X`, it is a code generated class that Uno.Extensions.Reactive gives us to provide the glue between what XAML Bindings expect, while allowing us to follow the MVU pattern.

Now let's update our UI in the MainPage.xaml to use the bindings.

```xml
<Page x:Class="SimpleCalculator.MainPage"
      xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
      xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
      xmlns:utu="using:Uno.Toolkit.UI"
      xmlns:um="using:Uno.Material"
      Background="{ThemeResource BackgroundBrush}">
  <!-- Page Resources excluded for clarity -->
  <utu:AutoLayout utu:SafeArea.Insets="VisibleBounds"
                  MaxWidth="700"
                  Padding="0,0,0,16"
                  Background="{ThemeResource BackgroundBrush}"
                  PrimaryAxisAlignment="End">
    <ToggleButton Background="{ThemeResource SecondaryContainerBrush}"
                  Margin="8"
                  utu:AutoLayout.CounterAlignment="Center"
                  Style="{StaticResource IconToggleButtonStyle}"
                  IsChecked="{Binding IsDark, Mode=TwoWay}"
                  CornerRadius="20">
      <ToggleButton.Content>
        <PathIcon Data="{StaticResource SunIcon}"
                  Foreground="{ThemeResource PrimaryVariantDarkBrush}" />
      </ToggleButton.Content>
      <um:ControlExtensions.AlternateContent>
        <PathIcon Data="{StaticResource MoonIcon}"
                  Foreground="{ThemeResource PrimaryVariantDarkBrush}" />
      </um:ControlExtensions.AlternateContent>
    </ToggleButton>
    <utu:AutoLayout Spacing="16"
                    Padding="16,8"
                    PrimaryAxisAlignment="End"
                    utu:AutoLayout.PrimaryAlignment="Stretch">
        <TextBlock Text="Equation"
                   utu:AutoLayout.CounterAlignment="End"
                   Foreground="{ThemeResource OnSecondaryContainerBrush}"
                   Style="{StaticResource DisplaySmall}" />
        <TextBlock Text="{Binding Output}"
                   utu:AutoLayout.CounterAlignment="End" Foreground="{ThemeResource OnBackgroundBrush}" Style="{StaticResource DisplayLarge}" />
    </utu:AutoLayout>
    <utu:AutoLayout MaxHeight="500" Spacing="16" Padding="16,0">
      <TextBox Text="{Binding Output}"
               utu:AutoLayout.CounterAlignment="Stretch"/>
      <Button Content="Press Me"
              utu:AutoLayout.CounterAlignment="Stretch" />
    </utu:AutoLayout>
  </utu:AutoLayout>
</Page>
```

With our bindings in place we can now run the app and see the theme switch work.
</details>

<details>
<summary>C# Markup</summary>

To start we need to set the DataContext, but we will want to make a change to the way that we initialize the UI so that we can create strongly typed bindings. It's also important to note here that we will not initialize the MainModel since we need the `X` in `MVU-X`. For this we will use the generated `BindableMainModel` class. Using the DataContext extension we can pass in a new instance of the `BindableMainModel` to set the DataContext and then we can provide a delegate with the Page and the provided DataContext type to use as we configure the Page content and configure bindings.

```cs
public MainPage()
{
    this.DataContext(new BindableMainModel(), (page, dataContext) => page
      .Content(...));
}
```

> **NOTE:** Binding expressions must be stateless and the model property in our lambda expression will *ALWAYS* have a null value. Attempting to access instance values from the model will result in a NullReferenceException.

Now we need to create the bindings.

```cs
public MainPage()
{
    this.DataContext(new BindableMainModel(), (page, dataContext) => page
        .Resources(r => r
            .Add(AppResources.Icon.Sun)
            .Add(AppResources.Icon.Moon))
        .Background(Theme.Brushes.Background.Default)
        .Content(new AutoLayout()
        .MaxWidth(700)
        .Padding(0, 0, 0, 16)
        .PrimaryAxisAlignment(AutoLayoutAlignment.End)
        .Children(
            new ToggleButton()
                .Margin(8)
                .CornerRadius(20)
                .AutoLayout(counterAlignment: AutoLayoutAlignment.Center)
                .Background(Theme.Brushes.Secondary.Container.Default)
                .Style(Theme.Styles.ToggleButton.Icon)
                .IsChecked(() => dataContext.IsDark) // Binding Expression
                .Content(new PathIcon()
                    .Data(AppResources.Icon.Sun))
                .ControlExtensions(alternateContent: new PathIcon()
                    .Data(AppResources.Icon.Moon)
                    .Foreground(Theme.Brushes.Primary.VariantDark.Default)),
            new AutoLayout()
                .Spacing(16)
                .Padding(16,8)
                .PrimaryAxisAlignment(AutoLayoutAlignment.End)
                .AutoLayout(primaryAlignment: AutoLayoutPrimaryAlignment.Stretch)
                .Children(
                    new TextBlock()
                        .Text("Equation")
                        .AutoLayout(counterAlignment: AutoLayoutAlignment.End)
                        .Foreground(Theme.Brushes.OnSecondary.Container.Default)
                        .Style(Theme.Styles.TextBlock.DisplaySmall),
                    new TextBlock()
                        .Text(() => dataContext.Output) // Binding Expression
                        .AutoLayout(counterAlignment: AutoLayoutAlignment.End)
                        .Foreground(Theme.Brushes.OnBackground.Default)
                        .Style(Theme.Styles.TextBlock.DisplayLarge)
                ),
            new AutoLayout()
                .MaxHeight(500)
                .Spacing(16)
                .Padding(16,0)
                .Children(
                    new TextBox()
                        .Text(() => dataContext.Output) // Binding Expression
                        .AutoLayout(counterAlignment: AutoLayoutAlignment.Stretch),
                    new Button()
                        .Content("Press Me")
                        .AutoLayout(counterAlignment: AutoLayoutAlignment.Stretch)
                )
        )));
}
```

With our bindings in place we can now run the app and see the theme switch work.
</details>

## Feeds &amp; Commands

In addition to the IState, sometimes we may need to create composite properties and execute commands. Reactive makes this simple with the IFeed and automatically detecting public methods. We will make a couple of changes to our MainModel to demonstrate this.

```cs
public partial record MainModel
{
    // Other properties left out for simplification
    public IFeed<string> Content { get; }
    public IState<int> Count { get; }

    public MainModel()
    {
        // Other properties left out for simplification
        Count = State<int>.Value(this, () => 0);
        Content = Feed<string>.Async(await _ => await Count switch
        {
            0 => "Press Me",
            1 => "Pressed Once",
            _ => $"Pressed {await Count} Times"
        });
    }

    public async Task PressMe(CancellationToken cancellationToken)
    {
        await Count.Set(1 + await Count, cancellationToken);
    }
}
```

<details>
<summary>XAML</summary>

Now we just need to update the Button in our UI as shown here.

```xml
<Button Content="{Binding Content}"
        Command="{Binding PressMe}"
        utu:AutoLayout.CounterAlignment="Stretch" />
```
</details>

<details>
<summary>C# Markup</summary>

Now we just need to update the Button in our UI as shown here.

```cs
new Button()
    .Content(() => dataContext.Content)
    .Command(() => dataContext.PressMe)
    .AutoLayout(counterAlignment: AutoLayoutAlignment.Stretch)
```
</details>

With our UI updated we can run the app again and Press the Button. We should see the text of the Button change to reflect the number of times it has been pressed.
