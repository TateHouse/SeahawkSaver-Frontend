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
	public event Action? OnChange;
	public UserModel User { get; } = new UserModel();

	public void Update(UserModel userModel)
	{
		User.UserId = userModel.UserId;
		User.Email = userModel.Email;
		User.FirstName = userModel.FirstName;
		User.LastName = userModel.LastName;
		User.IsAdmin = userModel.IsAdmin;
		User.IsActive = userModel.IsActive;

		OnChange?.Invoke();
	}

	public void Clear()
	{
		User.UserId = Guid.Empty;
		User.Email = string.Empty;
		User.FirstName = string.Empty;
		User.LastName = string.Empty;
		User.IsAdmin = false;
		User.IsActive = false;

		OnChange?.Invoke();
	}
}