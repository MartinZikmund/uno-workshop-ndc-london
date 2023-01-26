# Building your first project

Before we build our first app it's important to make a few decisions about how we would like to build our app. The template that we installed in the first module gives us a lot of options but we're just going to focus on a few to start.

## What is XAML

XAML is a XML based markup language that is used to define the structure of a user interface. First introduced by Microsoft for WPF applications, this has been a proven solution for building user interfaces. XAML is a declarative language, meaning that it describes the structure of the UI, but not how it should be rendered. This is done by the XAML processor, which is the XAML engine in the .NET framework. Due to it's nature, XAML is often preferred by developers coming from a web background as it is similar to HTML. However, XAML is not a replacement for HTML, and is not intended to be used for web development. It is important to understand that individual nodes in XAML map to actual .NET objects, and that the XAML processor is responsible for creating these objects and setting their properties.

## What is C# Markup

C# Markup is based on the desire by many developers to leverage better intellisense and strong typing that exists in C# code. With C# Markup we can build our UI entirely in C# using a generated set of Fluent Extensions that make it easier to build our UI. Additionally when using Uno Themes it becomes easier to discover available styles, colors and brushes.

## Creating your project

For this project we will be using the `unoapp-extensions` Template. As this template by default includes a number of features we don't need for this project we will be specifying some options to exclude them (`-di false -server false -unit-tests false -ui-tests false`). These options will disable all of the Uno Extensions which require dependency injection, as well as create the project without the Server (API) project and the Unit and UI Tests. We will also be specifying the `-id` option to set the Package Identifier for the project. This is important as it will be used to identify the app in the app store and is used to identify the app when installing it on a device. Be sure to update `com.<your company>.simplecalculator` to a unique identifier for your company.

By default the project will be created with XAML. If you would like to use C# Markup you can specify the `-ui csharp` option. If you would like to use the MVVM pattern you can specify the `-presentation mvvm` option.

<details>
<summary>XAML</summary>

```bash
# If using the MVU-X module
dotnet new unoapp-extensions -di false -server false -unit-tests false -ui-tests false -id com.<your company>.simplecalculator -o SimpleCalculator

# If using the MVVM module
dotnet new unoapp-extensions -presentation mvvm -di false -server false -unit-tests false -ui-tests false -id com.<your company>.simplecalculator -o SimpleCalculator
```

</details>

<details>
<summary>C# Markup</summary>

```bash
# If using the MVU-X module
dotnet new unoapp-extensions -ui csharp -di false -server false -unit-tests false -ui-tests false -id com.<your company>.simplecalculator -o SimpleCalculator

# If using the MVVM module
dotnet new unoapp-extensions -ui csharp -presentation mvvm -di false -server false -unit-tests false -ui-tests false -id com.<your company>.simplecalculator -o SimpleCalculator
```

</details>

## Additional Resources

- [C# Markup Docs](https://platform.uno/docs/articles/external/uno.extensions/doc/Overview/Hosting/HostingOverview.html)

## Next Steps

- [03 - Uno Toolkit](../03-Toolkit/README.md)