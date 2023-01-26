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

- [01 - Getting Started](#getting-Started)
- [02 - Build your first Project](modules/02-Build%20your%20first%20Project/README.md)
- [03 - Creating the Layout](modules/03-Creating%20the%20Layout/README.md)
- 04 Architecture
  - [MVVM](modules/04-App%20Architecture%20-%20MVVM/README.md)
  - [MVU-X](modules/04-App%20Architecture%20-%20MVU-X/README.md)

# Getting Started

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

## Additional Resources

- [Uno Docs - Getting Started](https://platform.uno/docs/articles/get-started.htm)

## Next Steps

- [02 - Build your first Project](../02-Build%20your%20first%20Project/README.md)
