namespace SeahawkSaverFrontend.UI.Features.User.Login.Services;
using AutoMapper;
using SeahawkSaverFrontend.UI.Features.Caching.Services;
using SeahawkSaverFrontend.UI.Features.User.DTOs;
using SeahawkSaverFrontend.UI.Features.User.Login.DTOs;
using System.Net.Http.Json;

/**
 * <summary>
 * A service for logging a user into the application.
 * </summary>
 */
public sealed class LoginService : ILoginService
{
	private readonly HttpClient httpClient;
	private readonly IMapper mapper;
	private readonly IDataCache dataCache;

	/**
	 * <summary>
	 * Instantiates a new <see cref="LoginService"/> instance.
	 * </summary>
	 * <param name="httpClient">The http client to use.</param>
	 * <param name="mapper">The mapper to use.</param>
	 * <param name="dataCache">The application's data cache.</param>
	 */
	public LoginService(HttpClient httpClient,
						IMapper mapper,
						IDataCache dataCache)
	{
		this.httpClient = httpClient;
		this.mapper = mapper;
		this.dataCache = dataCache;
	}

	public async Task<bool> LoginAsync(UserCredentialsModel model)
	{
		const string url = "http://localhost:5103/api/v1/user/login";
		var request = new
		{
			Email = model.Email,
			Password = model.Password
		};

		var response = await httpClient.PostAsJsonAsync(url, request);

		if (response.IsSuccessStatusCode == false)
		{
			return false;
		}

		var content = await response.Content.ReadFromJsonAsync<LoginUserEndpointResponse>();

		if (content == null)
		{
			return false;
		}

		var user = mapper.Map<UserModel>(content.User);
		dataCache.Token = content.Token;
		dataCache.User = user;

		return true;
	}
}