using Microsoft.UI.Xaml;
using Application = Microsoft.UI.Xaml.Application;

namespace SimpleCalculator;

public sealed partial class App : Application
{
	public App()
	{
		this.InitializeComponent();
	}

	/// <summary>
	/// Invoked when the application is launched normally by the end user.  Other entry points
	/// will be used such as when the application is launched to open a specific file.
	/// </summary>
	/// <param name="args">Details about the launch request and process.</param>
	protected override void OnLaunched(LaunchActivatedEventArgs args) =>
		AppStart.OnLaunched(this, args);
}
