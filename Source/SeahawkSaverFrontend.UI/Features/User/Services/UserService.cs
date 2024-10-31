namespace SeahawkSaverFrontend.UI.Features.User.Services;
using AutoMapper;
using SeahawkSaverFrontend.UI.Features.Caching.Services;
using SeahawkSaverFrontend.UI.Features.User.DTOs;
using System.Net.Http.Json;

public sealed class UserService : IUserService
{
	private readonly HttpClient httpClient;
	private readonly IMapper mapper;
	private readonly IDataCache dataCache;

	public UserService(HttpClient httpClient, IMapper mapper, IDataCache dataCache)
	{
		if (string.IsNullOrWhiteSpace(dataCache.Token))
		{
			throw new UnauthorizedAccessException();
		}

		this.httpClient = httpClient;
		this.mapper = mapper;
		this.dataCache = dataCache;

		AddBearerHeader();
	}

	public async Task<bool> UpdateUserAsync(UserModel userModel)
	{
		var url = $"http://localhost:5103/api/v1/user/{dataCache.User.UserId}";
		var content = new
		{
			User = new
			{
				Email = userModel.Email,
				FirstName = userModel.FirstName,
				LastName = userModel.LastName
			}
		};

		var request = JsonContent.Create(content);
		var response = await httpClient.PutAsync(url, request);

		if (response.IsSuccessStatusCode == false)
		{
			// TODO: Log an error message.

			return false;
		}

		return true;
	}

	private void AddBearerHeader()
	{
		if (httpClient.DefaultRequestHeaders.Contains("Bearer"))
		{
			return;
		}

		httpClient.DefaultRequestHeaders.Add("Bearer", dataCache.Token);
	}
}