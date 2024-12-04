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
	public UserModel? User { get; set; }
}