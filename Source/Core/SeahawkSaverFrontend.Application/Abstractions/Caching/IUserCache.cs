namespace SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Domain.Models.User;

/**
 * <summary>
 * An interface for a data cache containing user related data.
 * </summary>
 */
public interface IUserCache
{
	public event Action? OnChange;
	public UserModel User { get; }

	/**
	 * <summary>
	 * Updates the <see cref="User"/> in the cache with the properties from the specified <see cref="UserModel"/>.
	 * </summary>
	 * <param name="userModel">The properties to update the <see cref="User"/> in the cache with.</param>
	 */
	public void Update(UserModel userModel);

	/**
	 * <summary>
	 * Clears the <see cref="User"/> in the cache.
	 * </summary>
	 */
	public void Clear();
}