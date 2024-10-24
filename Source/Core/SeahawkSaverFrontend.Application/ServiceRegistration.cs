namespace SeahawkSaverFrontend.Application.UnitTest;
using Microsoft.Extensions.DependencyInjection;
using SeahawkSaverFrontend.Application.Abstractions.Application;
using SeahawkSaverFrontend.Application.Abstractions.Application.Services;
using SeahawkSaverFrontend.Application.Features.User.Commands.Login;

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
		services.AddSingleton(typeof(IDataCache<>), typeof(InMemoryDataCache<>));
		services.AddScoped<ILoginService, LoginService>();

		return services;
	}
}