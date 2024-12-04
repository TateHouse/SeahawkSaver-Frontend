namespace SeahawkSaverFrontend.Application.Features.Caching;
using SeahawkSaverFrontend.Application.Abstractions.Caching;

/**
 * <summary>
 * An in-memory data cache containing authentication related data.
 * </summary>
 */
public sealed class InMemoryAuthenticationCache : IAuthenticationCache
{
	private string token = string.Empty;

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
			OnChange?.Invoke();

		}
	}
}