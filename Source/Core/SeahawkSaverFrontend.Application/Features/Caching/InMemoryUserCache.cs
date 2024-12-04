namespace SeahawkSaverFrontend.Application.Features.Caching;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Domain.Models.User;

/**
 * <summary>
 * An in-memory data cache containing user related data.
 * </summary>
 */
public sealed class InMemoryUserCache : IUserCache
{
	private UserModel? user;

	public event Action? OnChange;

	public UserModel? User
	{
		get
		{
			return user;
		}

		set
		{
			user = value;
			OnChange?.Invoke();
		}
	}
}