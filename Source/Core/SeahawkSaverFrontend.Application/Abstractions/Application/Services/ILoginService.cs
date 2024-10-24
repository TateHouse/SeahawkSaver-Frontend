namespace SeahawkSaverFrontend.Application.Abstractions.Application.Services;
public interface ILoginService
{
	public Task<bool> LoginAsync(string email, string password);
}