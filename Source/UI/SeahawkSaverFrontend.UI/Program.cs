namespace SeahawkSaverFrontend.UI;
public class Program
{
	public async static Task Main(string[] args)
	{
		var webApplicationBuilder = WebApplication.CreateBuilder(args);
		webApplicationBuilder.ConfigureServices();

		var application = webApplicationBuilder.Build();
		application.ConfigureMiddleware();
		await application.RunAsync();
	}
}