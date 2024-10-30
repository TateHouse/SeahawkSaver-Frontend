using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace SeahawkSaverFrontend.UI;
using MudBlazor.Services;
using SeahawkSaverFrontend.UI.Features.Caching.Services;
using SeahawkSaverFrontend.UI.Features.Income.Services;
using SeahawkSaverFrontend.UI.Features.User.Login.Services;
using SeahawkSaverFrontend.UI.Features.User.LogOut.Services;

public class Program
{
	public async static Task Main(string[] args)
	{
		var builder = WebAssemblyHostBuilder.CreateDefault(args);
		builder.RootComponents.Add<App>("#app");
		builder.RootComponents.Add<HeadOutlet>("head::after");

		builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
		builder.Services.AddMudServices();
		builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
		builder.Services.AddSingleton<IDataCache, InMemoryDataCache>();
		builder.Services.AddScoped<ILoginService, LoginService>();
		builder.Services.AddScoped<ILogOutService, LogOutService>();
		builder.Services.AddScoped<IIncomeService, IncomeService>();

		await builder.Build().RunAsync();
	}
}