namespace SeahawkSaverFrontend.Application.Features.UseCases.User;
using Microsoft.Extensions.DependencyInjection;
using SeahawkSaverFrontend.Application.Features.UseCases.User.Login;
using SeahawkSaverFrontend.Application.Features.UseCases.User.Logout;
using SeahawkSaverFrontend.Application.Features.UseCases.User.Update;
using SeahawkSaverFrontend.Domain.Models.User;

/**
 * <summary>
 * A class for registering the <see cref="UserModel"/> related use cases.
 * </summary>
 */
public static class UseCaseRegistration
{
	/**
	 * <summary>
	 * An extension method for <see cref="IServiceCollection"/> to register the <see cref="UserModel"/> related use
	 * cases.
	 * </summary>
	 */
	internal static void RegisterUserUseCases(this IServiceCollection services)
	{
		services.AddTransient<LoginUserModelUseCase>();
		services.AddTransient<LogoutUserModelUseCase>();
		services.AddTransient<UpdateUserModelUseCase>();
	}
}