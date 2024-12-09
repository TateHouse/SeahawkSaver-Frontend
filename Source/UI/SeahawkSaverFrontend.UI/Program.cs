namespace SeahawkSaverFrontend.UI;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

public static class Program
{
	public async static Task Main(string[] args)
	{
		var builder = WebAssemblyHostBuilder.CreateDefault(args);
		builder.ConfigureServices();

		var host = builder.Build();
		await host.RunAsync();
	}
}