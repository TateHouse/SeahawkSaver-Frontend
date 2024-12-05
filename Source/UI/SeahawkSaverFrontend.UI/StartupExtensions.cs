namespace SeahawkSaverFrontend.UI;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using SeahawkSaverFrontend.Application;

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

		builder.Services.AddMudServices();

		builder.Services.RegisterApplicationServices();

		return builder;
	}
}