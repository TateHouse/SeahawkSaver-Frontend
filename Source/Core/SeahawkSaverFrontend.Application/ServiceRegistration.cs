namespace SeahawkSaverFrontend.Application;
using Microsoft.Extensions.DependencyInjection;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Abstractions.UseCases;
using SeahawkSaverFrontend.Application.Features.Caching;
using SeahawkSaverFrontend.Application.Features.UseCases;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Debt;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Expense;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Income;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Saving;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Subscription;
using SeahawkSaverFrontend.Application.Utilities;

/**
 * <summary>
 * A class for registering services.
 * </summary>
 */
public static class ServiceRegistration
{
	/**
	 * <summary>
	 * An extension method for <see cref="IServiceCollection"/> to register the <see cref="Application"/> services.
	 * </summary>
	 */
	public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
	{
		services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
		services.AddTransient<ApiHttpClient>();

		RegisterCaches(services);
		RegisterUseCases(services);

		return services;
	}

	/**
	 * <summary>
	 * Registers the caches.
	 * </summary>
	 * <param name="services">The service collection to use.</param>
	 */
	private static void RegisterCaches(IServiceCollection services)
	{
		services.AddSingleton<IUserCache, InMemoryUserCache>();
		services.AddSingleton<IAuthenticationCache, InMemoryAuthenticationCache>();
		services.AddSingleton(typeof(IFinancialModelCache<>), typeof(InMemoryFinancialModelCache<>));
	}

	/**
	 * <summary>
	 * Registers the use cases.
	 * </summary>
	 * <param name="services">The service collection to use.</param>
	 */
	private static void RegisterUseCases(IServiceCollection services)
	{
		services.AddTransient<IUseCaseFactory, UseCaseFactory>();
		services.RegisterDebtUseCases();
		services.RegisterExpenseUseCases();
		services.RegisterIncomeUseCases();
		services.RegisterSavingUseCases();
		services.RegisterSubscriptionUseCases();
	}
}