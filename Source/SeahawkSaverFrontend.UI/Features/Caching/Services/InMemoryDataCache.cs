namespace SeahawkSaverFrontend.UI.Features.Caching.Services;
using SeahawkSaverFrontend.UI.Features.User.DTOs;

/**
 * <summary>
 * An in-memory data cache which will be cleaned up once the user closes the browser.
 * </summary>
 */
public sealed class InMemoryDataCache : IDataCache
{
	private string token = string.Empty;
	private UserModel user = new UserModel();

	public event Action? OnChange;

	public string Token
	{
		get
		{
			return token;
		}
		set
		{
			token = value;
			NotifyStateChanged();
		}
	}

	public UserModel User
	{
		get
		{
			return user;
		}
		set
		{
			user = value;
			NotifyStateChanged();
		}
	}

	private void NotifyStateChanged()
	{
		OnChange?.Invoke();
	}
}