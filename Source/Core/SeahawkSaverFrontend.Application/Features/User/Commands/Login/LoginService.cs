namespace SeahawkSaverFrontend.Application.Features.User.Commands.Login;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using SeahawkSaverFrontend.Application.Abstractions.Application;
using SeahawkSaverFrontend.Application.Abstractions.Application.Services;
using SeahawkSaverFrontend.Application.Features.User.Commands.Login.DTOs;
using SeahawkSaverFrontend.Domain.Models;
using System.Net.Http.Json;

public sealed class LoginService : ILoginService
{
	private readonly IConfiguration configuration;
	private readonly IMapper mapper;
	private readonly IHttpClientFactory httpClientFactory;
	private readonly IDataCache<User> userInMemoryDataCache;
	private readonly IDataCache<Token> tokenInMemoryDataCache;

	public LoginService(IConfiguration configuration,
						IMapper mapper,
						IHttpClientFactory httpClientFactory,
						IDataCache<User> userInMemoryDataCache,
						IDataCache<Token> tokenInMemoryDataCache)
	{
		this.configuration = configuration;
		this.mapper = mapper;
		this.httpClientFactory = httpClientFactory;
		this.userInMemoryDataCache = userInMemoryDataCache;
		this.tokenInMemoryDataCache = tokenInMemoryDataCache;
	}

	public async Task<bool> LoginAsync(string email, string password)
	{
		var baseUrl = configuration.GetValue<string>("ApiSettings:Url") ?? throw new InvalidOperationException("The ApiSettings:Url must be provided.");
		var loginUrl = $"{baseUrl}/user/login";
		var request = new
		{
			Email = email,
			Password = password
		};

		var httpClient = httpClientFactory.CreateClient();
		var response = await httpClient.PostAsJsonAsync(loginUrl, request);

		if (response.IsSuccessStatusCode == false)
		{
			return false;
		}

		var content = await response.Content.ReadFromJsonAsync<LoginUserEndpointResponse>();

		if (content == null)
		{
			return false;
		}

		var user = mapper.Map<User>(content.User);
		var token = mapper.Map<Token>(content.Token);

		userInMemoryDataCache.UpdateCache(user);
		tokenInMemoryDataCache.UpdateCache(token);

		return true;
	}
}