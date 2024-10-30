namespace SeahawkSaverFrontend.UI.Features.Caching.Services;
using SeahawkSaverFrontend.UI.Features.User.DTOs;

/**
 * <summary>
 * An interface for a data cache containing certain information about the logged-in user.
 * </summary>
 */
public interface IDataCache
{
	public event Action? OnChange;
	public string Token { get; set; }
	public UserModel User { get; set; }
}