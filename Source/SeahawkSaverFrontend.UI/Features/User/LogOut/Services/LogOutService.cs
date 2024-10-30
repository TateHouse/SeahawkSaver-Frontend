namespace SeahawkSaverFrontend.UI.Features.User.LogOut.Services;
using SeahawkSaverFrontend.UI.Features.Caching.Services;
using SeahawkSaverFrontend.UI.Features.User.DTOs;

public sealed class LogOutService : ILogOutService
{
	private readonly IDataCache dataCache;

	public LogOutService(IDataCache dataCache)
	{
		this.dataCache = dataCache;
	}

	public void LogOut()
	{
		dataCache.Token = "";
		dataCache.User = new UserModel();
	}
}