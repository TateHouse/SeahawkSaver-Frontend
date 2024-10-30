namespace SeahawkSaverFrontend.UI.Features.User.Login.Services;
using SeahawkSaverFrontend.UI.Features.User.Login.DTOs;

/**
 * <summary>
 * An interface for a service to log in a user into the application.
 * </summary>
 */
public interface ILoginService
{
	/**
	 * <summary>
	 * Logs the user into the application.
	 * </summary>
	 * <param name="model">The user's provided credentials.</param>
	 * <returns>A task that represents the asynchronous operation, and it contains true if the operation was successful.
	 * Otherwise, it contains false.</returns>
	 */
	public Task<bool> LoginAsync(UserCredentialsModel model);
}