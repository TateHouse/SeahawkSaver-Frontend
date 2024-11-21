namespace SeahawkSaverFrontend.UI.Features.User.Services;
using SeahawkSaverFrontend.UI.Features.User.DTOs;

public interface IUserService
{
	public Task<IEnumerable<UserModel>> GetUsersAsync();
	public Task<bool> UpdateUserAsync(UserModel userModel);
}