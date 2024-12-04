namespace SeahawkSaverFrontend.Application.Abstractions.Caching;
/**
 * <summary>
 * An interface for a data cache containing authentication related data.
 * </summary>
 */
public interface IAuthenticationCache
{
	public event Action? OnChange;
	public string Token { get; set; }
}