using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace SeahawkSaverFrontend.UI;
using MudBlazor.Services;
using SeahawkSaverFrontend.UI.Features.Caching.Services;
using SeahawkSaverFrontend.UI.Features.Debt.Services;
using SeahawkSaverFrontend.UI.Features.Expense.Services;
using SeahawkSaverFrontend.UI.Features.Income.Services;
using SeahawkSaverFrontend.UI.Features.Saving.Services;
using SeahawkSaverFrontend.UI.Features.Subscription.Services;
using SeahawkSaverFrontend.UI.Features.User.Login.Services;
using SeahawkSaverFrontend.UI.Features.User.LogOut.Services;
using SeahawkSaverFrontend.UI.Features.User.Services;

public class Program
{
	public async static Task Main(string[] args)
	{
		var builder = WebAssemblyHostBuilder.CreateDefault(args);
		builder.RootComponents.Add<App>("#app");
		builder.RootComponents.Add<HeadOutlet>("head::after");

		builder.Services.AddTransient(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
		builder.Services.AddMudServices();
		builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
		builder.Services.AddSingleton<IDataCache, InMemoryDataCache>();
		builder.Services.AddTransient<ILoginService, LoginService>();
		builder.Services.AddTransient<ILogOutService, LogOutService>();
		builder.Services.AddTransient<IUserService, UserService>();
		builder.Services.AddTransient<IDebtService, DebtService>();
		builder.Services.AddTransient<IIncomeService, IncomeService>();
		builder.Services.AddTransient<ISavingService, SavingService>();
		builder.Services.AddTransient<ISubscriptionService, SubscriptionService>();
		builder.Services.AddTransient<IExpenseService, ExpenseService>();

		await builder.Build().RunAsync();
	}
}