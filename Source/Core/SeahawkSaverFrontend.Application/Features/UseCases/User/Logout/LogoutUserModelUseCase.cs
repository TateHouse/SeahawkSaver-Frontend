namespace SeahawkSaverFrontend.Application.Features.UseCases.User.Logout;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Abstractions.UseCases;
using SeahawkSaverFrontend.Domain.Models.User;

/**
 * <summary>
 * A use case for logging out from the application.
 * </summary>
 */
public sealed class LogoutUserModelUseCase : UseCase<object?, bool>
{
	private readonly IUserCache userCache;
	private readonly IAuthenticationCache authenticationCache;

	// TODO: Add the financial model caches.

	/**
	 * <summary>
	 * Instantiates a new <see cref="LogoutUserModelUseCase"/>
	 * instance.
	 * </summary>
	 * <param name="userCache">The user cache to use.</param>
	 * <param name="authenticationCache">The authentication cache to use.</param>
	 */
	public LogoutUserModelUseCase(IUserCache userCache, IAuthenticationCache authenticationCache)
	{
		this.userCache = userCache;
		this.authenticationCache = authenticationCache;
	}

	public override Task<bool> ExecuteAsync(object? input)
	{
		authenticationCache.Token = "";
		userCache.User = new UserModel();

		return Task.FromResult(true);
	}
}