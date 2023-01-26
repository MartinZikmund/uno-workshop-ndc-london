# Simple Calc Workshop

The Simple Calc Workshop is here to help you get started building an app for Desktop, Mobile and Web with Uno Platform. This workshop is designed to help you get your developer environment set up to start building your first Uno App while learning about the tools, libraries and patterns that will help you to build your apps with Uno Platform.

## What you will learn

- How to prepare your environment to build cross platform apps with Uno Platform whether you're using Visual Studio, Visual Studio for Mac, or Visual Studio Code.
- How to create a new solution using the Uno Platform templates.
- What the Uno Toolkit offers and how it can help you build pixel perfect apps cross platform
- How to get started with the Uno Themes for Fluent, Material or Cupertino
- How to build your app using XAML or C# Markup
- How to build your app using Model-View-ViewModel (MVVM) or Model-View-Update for XAML (MVU-X)
- Optionally: How you can accelerate your development by coordinating with you UI Designer using Figma.

This workshop has been setup to provide you with optional content to allow you to tailor the experience to your needs.

## Prerequisites

- A working understanding of C# / .NET
- Visual Studio 2022 (Windows or Mac)
- Visual Studio Code (optional)

### Good to have

- A working understanding of XAML
- A working understanding of MVVM

## Modules

- [01 - Getting Started](#1-getting-Started)
- [02 - Build your first Project](modules/02-Build%20your%20first%20Project/README.md)
- [03 - Creating the Layout](modules/03-Creating%20the%20Layout/README.md)
- 04 Architecture
  - [MVVM](modules/04-App%20Architecture%20-%20MVVM/README.md)
  - [MVU-X](modules/04-App%20Architecture%20-%20MVU-X/README.md)

# 1. Getting Started

Uno Platform provides a cross platform solution for building native apps for iOS, Android, Windows, macOS, WebAssembly as well as Linux FrameBuffer and GTK. This module will walk you through the process of getting started with Uno Platform.

## Pre-requisites

You should already have a working understanding of C# with one of the following IDEs:

- Visual Studio 2022 (Windows or Mac)
- Visual Studio Code
- JetBrains Rider

## Setting up the Environment

If you are using Visual Studio, depending on the workloads that you have installed your environment may already be ready to go. As a best practice or to help solve issues following Visual Studio updates, we recommend that you run the Uno Check tool to ensure that your environment is ready to go. From the Terminal

```bash
dotnet tool install --global Uno.Check
```

If you previously installed Uno.Check, make sure it is up to date with:

```bash
dotnet tool update --global Uno.Check
```

When ready, run the tool with:

```
uno-check
```

On a mac, you may need to run the command this way:

```
~/.dotnet/tools/uno-check
```

> **NOTE:** You may need to take additional steps if trying to build the Linux or GTK heads on Windows. Be sure to follow the [Additional setup for Linux or WSL](https://platform.uno/docs/articles/get-started-with-linux.html?tabs=ubuntu1804) docs.

## Getting the Project Templates

Uno Platform provides a robust template system that allows you to create projects with the features that you need. To get started we will continue from the Terminal and install the Uno Platform Extensions Template.

```bash
dotnet new install Uno.Extensions.Templates::2.3.0-dev.572
```

> **NOTE**: We are installing the preview version currently, soon 2.3 will be released as stable.

Now that we have the template installed let's take a look at the options before we create a project.

```bash
dotnet new unoapp-extensions -h
```

# Building your first project

Before we build our first app it's important to make a few decisions about how we would like to build our app. The template that we installed in the first module gives us a lot of options but we're just going to focus on a few to start.

## What is XAML

XAML is a XML based markup language that is used to define the structure of a user interface. First introduced by Microsoft for WPF applications, this has been a proven solution for building user interfaces. XAML is a declarative language, meaning that it describes the structure of the UI, but not how it should be rendered. This is done by the XAML processor, which is the XAML engine in the .NET framework. Due to it's nature, XAML is often preferred by developers coming from a web background as it is similar to HTML. However, XAML is not a replacement for HTML, and is not intended to be used for web development. It is important to understand that individual nodes in XAML map to actual .NET objects, and that the XAML processor is responsible for creating these objects and setting their properties.

## What is C# Markup

C# Markup is based on the desire by many developers to leverage better intellisense and strong typing that exists in C# code. With C# Markup we can build our UI entirely in C# using a generated set of Fluent Extensions that make it easier to build our UI. Additionally when using Uno Themes it becomes easier to discover available styles, colors and brushes.

## Creating your project

For this project we will be using the `unoapp-extensions` Template. As this template by default includes a number of features we don't need for this project we will be specifying some options to exclude them (`-di false -server false -unit-tests false -ui-tests false`). These options will disable all of the Uno Extensions which require dependency injection, as well as create the project without the Server (API) project and the Unit and UI Tests. We will also be specifying the `-id` option to set the Package Identifier for the project. This is important as it will be used to identify the app in the app store and is used to identify the app when installing it on a device. Be sure to update `com.<your company>.simplecalculator` to a unique identifier for your company.

By default the project will be created with XAML. If you would like to use C# Markup you can specify the `-ui csharp` option. If you would like to use the MVVM pattern you can specify the `-presentation mvvm` option.

```bash
# If using the MVU-X module
dotnet new unoapp-extensions -di false -server false -unit-tests false -ui-tests false -id com.<your company>.simplecalculator -o SimpleCalculator

# If using the MVVM module
dotnet new unoapp-extensions -presentation mvvm -di false -server false -unit-tests false -ui-tests false -id com.<your company>.simplecalculator -o SimpleCalculator
```

# Creating the Layout

As we start we will need to open the MainPage.xaml and add the following resources to the Page. These resources will be used later for our switch between Light and Dark themes.

```xml
<Page.Resources>
  <x:String x:Key="MoonIcon">F1 M 3 0 C 1.9500000476837158 0 0.949999988079071 0.1600000262260437 0 0.46000003814697266 C 4.059999942779541 1.7300000190734863 7 5.519999980926514 7 10 C 7 14.480000019073486 4.059999942779541 18.27000093460083 0 19.540000915527344 C 0.949999988079071 19.840000927448273 1.9500000476837158 20 3 20 C 8.519999980926514 20 13 15.519999980926514 13 10 C 13 4.480000019073486 8.519999980926514 0 3 0 Z</x:String>
  <x:String x:Key="SunIcon">F1 M 5.760000228881836 4.289999961853027 L 3.9600000381469727 2.5 L 2.549999952316284 3.9100000858306885 L 4.340000152587891 5.699999809265137 L 5.760000228881836 4.289999961853027 Z M 3 9.949999809265137 L 0 9.949999809265137 L 0 11.949999809265137 L 3 11.949999809265137 L 3 9.949999809265137 Z M 12 0 L 10 0 L 10 2.950000047683716 L 12 2.950000047683716 L 12 0 L 12 0 Z M 19.450000762939453 3.9100000858306885 L 18.040000915527344 2.5 L 16.25 4.289999961853027 L 17.65999984741211 5.699999809265137 L 19.450000762939453 3.9100000858306885 Z M 16.239999771118164 17.610000610351562 L 18.030000686645508 19.40999984741211 L 19.440000534057617 18 L 17.639999389648438 16.21000099182129 L 16.239999771118164 17.610000610351562 Z M 19 9.949999809265137 L 19 11.949999809265137 L 22 11.949999809265137 L 22 9.949999809265137 L 19 9.949999809265137 Z M 11 4.949999809265137 C 7.690000057220459 4.949999809265137 5 7.639999866485596 5 10.949999809265137 C 5 14.259999752044678 7.690000057220459 16.950000762939453 11 16.950000762939453 C 14.309999942779541 16.950000762939453 17 14.259999752044678 17 10.949999809265137 C 17 7.639999866485596 14.309999942779541 4.949999809265137 11 4.949999809265137 Z M 10 21.900001525878906 L 12 21.900001525878906 L 12 18.950000762939453 L 10 18.950000762939453 L 10 21.900001525878906 Z M 2.549999952316284 17.990001678466797 L 3.9600000381469727 19.400001525878906 L 5.75 17.600000381469727 L 4.340000152587891 16.190000534057617 L 2.549999952316284 17.990001678466797 Z</x:String>
</Page.Resources>
```

Great, now our UI is updated and is ready for Data Binding. We will pick up with Data Binding in the next section.

# Uno Toolkit

The Uno Toolkit provides a number of controls and attached properties that help us build pixel perfect apps. For the purposes of the Simple Calc we will be looking at 2 primary things from the Toolkit. For more information on the Toolkit be sure to check out the [Toolkit docs](https://platform.uno/docs/articles/external/uno.toolkit.ui/doc/getting-started.html)

## Installing the Toolkit

From Visual Studio or Visual Studio for Mac open the Package Manager and locate the package listed for either XAML or C# Markup. For those developing from Visual Studio Code please click on the link for the package to determine the latest available package and add the PackageReference to the SimpleCalc.csproj

Install [Uno.Toolkit.WinUI](https://www.nuget.org/packages/Uno.Toolkit.WinUI/)

<picture>
  <source media="(prefers-color-scheme: dark)" srcset="../../art/Dark/UnoToolkitWinUI.png">
  <source media="(prefers-color-scheme: light)" srcset="../../art/Light/UnoToolkitWinUI.png">
  <img alt="Install Uno.Toolkit.WinUI" src="../../art/Light/UnoToolkitWinUI.png">
</picture>

Next open the App.xaml in the IDE and add the ToolkitResources as shown below to the Merged Dictionaries

```xml
<Application x:Class="UnoExtApp120.App"
			 xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
			 xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
			 xmlns:wasm="http://platform.uno/wasm"
			 xmlns:local="using:UnoExtApp120"
			 xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
			 mc:Ignorable="wasm">

	<Application.Resources>
		<ResourceDictionary>
			<ResourceDictionary.MergedDictionaries>
				<!-- Load WinUI resources -->
				<XamlControlsResources xmlns="using:Microsoft.UI.Xaml.Controls" />

				<!-- Load Uno Toolkit resources -->
				<ToolkitResources xmlns="using:Uno.Toolkit.UI" />
			</ResourceDictionary.MergedDictionaries>
			<!-- Add resources here -->
		</ResourceDictionary>
	</Application.Resources>

</Application>
```

## AutoLayout

For developers coming from other platforms it is easiest to think of the AutoLayout like a Flex Layout that you may be familiar with in web development or other UI Frameworks. For more information about the AutoLayout be sure to check out the [docs](https://platform.uno/docs/articles/external/uno.toolkit.ui/doc/controls/AutoLayoutControl.html).

For our layout we will start by adding an AutoLayout as the root which will contain our UI. Next we will add a ToggleButton centered at the top of our layout and two more AutoLayouts which we will later update to contain the Display and the Buttons for our Calculator.

```xml
<Page x:Class="SimpleCalculator.MainPage"
      xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
      xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
      xmlns:utu="using:Uno.Toolkit.UI">
  <Page.Resources>
    <x:String x:Key="SunIcon">F1 M 5.760000228881836 4.289999961853027 L 3.9600000381469727 2.5 L 2.549999952316284 3.9100000858306885 L 4.340000152587891 5.699999809265137 L 5.760000228881836 4.289999961853027 Z M 3 9.949999809265137 L 0 9.949999809265137 L 0 11.949999809265137 L 3 11.949999809265137 L 3 9.949999809265137 Z M 12 0 L 10 0 L 10 2.950000047683716 L 12 2.950000047683716 L 12 0 L 12 0 Z M 19.450000762939453 3.9100000858306885 L 18.040000915527344 2.5 L 16.25 4.289999961853027 L 17.65999984741211 5.699999809265137 L 19.450000762939453 3.9100000858306885 Z M 16.239999771118164 17.610000610351562 L 18.030000686645508 19.40999984741211 L 19.440000534057617 18 L 17.639999389648438 16.21000099182129 L 16.239999771118164 17.610000610351562 Z M 19 9.949999809265137 L 19 11.949999809265137 L 22 11.949999809265137 L 22 9.949999809265137 L 19 9.949999809265137 Z M 11 4.949999809265137 C 7.690000057220459 4.949999809265137 5 7.639999866485596 5 10.949999809265137 C 5 14.259999752044678 7.690000057220459 16.950000762939453 11 16.950000762939453 C 14.309999942779541 16.950000762939453 17 14.259999752044678 17 10.949999809265137 C 17 7.639999866485596 14.309999942779541 4.949999809265137 11 4.949999809265137 Z M 10 21.900001525878906 L 12 21.900001525878906 L 12 18.950000762939453 L 10 18.950000762939453 L 10 21.900001525878906 Z M 2.549999952316284 17.990001678466797 L 3.9600000381469727 19.400001525878906 L 5.75 17.600000381469727 L 4.340000152587891 16.190000534057617 L 2.549999952316284 17.990001678466797 Z</x:String>
  </Page.Resources>
  <utu:AutoLayout MaxWidth="700"
                  Padding="0,0,0,16"
                  PrimaryAxisAlignment="End">
    <ToggleButton Margin="8"
                  utu:AutoLayout.CounterAlignment="Center"
                  CornerRadius="20">
      <ToggleButton.Content>
        <PathIcon Data="{StaticResource SunIcon}" />
      </ToggleButton.Content>
    </ToggleButton>
    <utu:AutoLayout Spacing="16" Padding="16,8" PrimaryAxisAlignment="End" utu:AutoLayout.PrimaryAlignment="Stretch">
        <TextBlock Text="Equation"
                   utu:AutoLayout.CounterAlignment="End" />
        <TextBlock Text="Output"
                   utu:AutoLayout.CounterAlignment="End" />
    </utu:AutoLayout>
    <utu:AutoLayout MaxHeight="500" Spacing="16" Padding="16,0">
      <TextBox Text="Some Text"
               utu:AutoLayout.CounterAlignment="Stretch"/>
      <Button Content="Press Me"
              utu:AutoLayout.CounterAlignment="Stretch" />
    </utu:AutoLayout>
  </utu:AutoLayout>
</Page>
```

Now that you have basic Layout, run the app and take a look at how it looks. Depending on whether you are on PC or Mac try out some of the various targets available to you.

## Safe Area

Safe Area is a concept that is used to ensure that the UI is not covered by the device's status bar or navigation bar (or notch). For more information about the SafeArea be sure to check out the [docs](https://platform.uno/docs/articles/external/uno.toolkit.ui/doc/controls/SafeArea.html).

In order to make use of the SafeArea we need to update the root AutoLayout by using the `Insets` attached property set to `VisibleBounds`


```xml
<Page x:Class="SimpleCalculator.MainPage"
      xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
      xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
      xmlns:utu="using:Uno.Toolkit.UI">
  <utu:AutoLayout utu:SafeArea.Insets="VisibleBounds"
                  MaxWidth="700"
                  Padding="0,0,0,16"
                  PrimaryAxisAlignment="End">
    <!-- Your Content -->
  </utu:AutoLayout>
</Page>
```

# 3. App Architecture

# 3a. Model View Update for Xaml (MVU-X)

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


Now we just need to update the Button in our UI as shown here.

```xml
<Button Content="{Binding Content}"
        Command="{Binding PressMe}"
        utu:AutoLayout.CounterAlignment="Stretch" />
```

With our UI updated we can run the app again and Press the Button. We should see the text of the Button change to reflect the number of times it has been pressed.

# 3b. Model View ViewModel (MVVM)

Model-View-ViewModel is a well established pattern for building applications. It is a pattern that is well suited to the Uno Platform, and is a great way to build your apps. In this module we will be looking at how to use the MVVM pattern to build our UI. We will be using the CommunityToolkit.MVVM to help us build our UI. It is important to note that while we will be using the CommunityToolkit that there are a number of other MVVM Frameworks that you can use with Uno Platform.

To start we need to install the CommunityToolkit.Mvvm NuGet package.

<picture>
  <source media="(prefers-color-scheme: dark)" srcset="../../art/Dark/CommunityToolkitMvvm.png">
  <source media="(prefers-color-scheme: light)" srcset="../../art/Light/CommunityToolkitMvvm.png">
  <img alt="Install Uno.Toolkit.WinUI" src="../../art/Light/CommunityToolkitMvvm.png">
</picture>

## Getting Started

To start off we will need to create a new model called `MainViewModel` and add a couple of properties to it. We'll add two properties that we will then bind to in our View. For our IsDark property we will need some special logic when IsDark is changed, for this property we will need to fully implement it. However for our Output property we can simply provide the private backing field and let the source generator generate the boilerplate for the public facing property by marking the private field with the `[ObservableProperty]` attribute.

```cs
public partial class MainViewModel : ObservableObject
{
    private bool _isDark = AppThemeService.Instance.IsDark;
    public bool IsDark
    {
        get => _isDark;
        set => SetProperty(ref _isDark, value, dark => { AppThemeService.Instance.SetThemeAsync(dark, default); });
    }

    [ObservableProperty]
    private string _output = string.Empty;
}
```

## Binding to properties in the UI

With our ViewModel created we will now need to set up our DataContext and create some bindings.

To start let's set the DataContext in the MainPage.xaml.cs (code behind).

```cs
public partial class MainPage : Page
{
    public MainPage()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }
}
```

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

## Commands

In addition to properties, sometimes we may need to create and execute commands. For this we will add a couple of properties to our `MainViewModel` and then update the `Button` in our UI to use the `Command` property.

```cs
public partial class MainViewModel : ObservableObject
{
    // Other properties left out for simplification
    [ObservableProperty]
    private string _content = "Press Me";
    private int _count;

    [RelayCommand]
    private void PressMe() =>
        Content = ++_count switch
        {
            1 => "Pressed Once",
            _ => $"Pressed {_count} Times"
        };
}
```

Now we just need to update the Button in our UI as shown here.

```xml
<Button Content="{Binding Content}"
        Command="{Binding PressMeCommand}"
        utu:AutoLayout.CounterAlignment="Stretch" />
```

With our UI updated we can run the app again and Press the Button. We should see the text of the Button change to reflect the number of times it has been pressed.
