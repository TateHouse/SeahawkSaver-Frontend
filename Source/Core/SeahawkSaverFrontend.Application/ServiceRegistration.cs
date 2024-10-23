namespace SeahawkSaverFrontend.Application.UnitTest;
using Microsoft.Extensions.DependencyInjection;

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

		return services;
	}
}