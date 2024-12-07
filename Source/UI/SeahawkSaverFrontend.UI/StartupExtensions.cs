namespace SeahawkSaverFrontend.UI;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using SeahawkSaverFrontend.Application;
using SeahawkSaverFrontend.UI.Components.Financial;
using SeahawkSaverFrontend.UI.Components.Financial.Calendar.UseCases;

/**
 * <summary>
 * Extension methods for configuring the services.
 * </summary>
 */
public static class StartupExtensions
{
	/**
	 * <summary>
	 * An extension method for <see cref="WebAssemblyHostBuilder"/> to configure the services.
	 * </summary>
	 */
	public static WebAssemblyHostBuilder ConfigureServices(this WebAssemblyHostBuilder builder)
	{
		builder.RootComponents.Add<App>("#app");
		builder.RootComponents.Add<HeadOutlet>("head::after");

		builder.Services.AddTransient(_ => new HttpClient
		{
			BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
		});

		builder.Services.AddMudServices(configuration =>
		{
			configuration.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
			configuration.SnackbarConfiguration.PreventDuplicates = false;
			configuration.SnackbarConfiguration.ShowCloseIcon = true;
			configuration.SnackbarConfiguration.VisibleStateDuration = 2500;
			configuration.SnackbarConfiguration.HideTransitionDuration = 125;
			configuration.SnackbarConfiguration.ShowTransitionDuration = 125;
			configuration.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
		});

		builder.Services.RegisterApplicationServices();
		builder.Services.RegisterCalendarUseCases();
		builder.Services.RegisterFinancialComponentServices();

		return builder;
	}
}