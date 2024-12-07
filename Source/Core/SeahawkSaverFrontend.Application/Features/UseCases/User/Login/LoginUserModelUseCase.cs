namespace SeahawkSaverFrontend.Application.Features.UseCases.User.Login;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Abstractions.UseCases;
using SeahawkSaverFrontend.Application.Features.UseCases.User.Login.DTOs;
using SeahawkSaverFrontend.Application.Utilities;
using SeahawkSaverFrontend.Domain.Models.User;
using System.Net.Http.Json;

/**
 * <summary>
 * A use case for logging into the application.
 * </summary>
 */
public sealed class LoginUserModelUseCase : UseCase<UserCredentialsModel, bool>
{
	private readonly HttpClient httpClient;
	private readonly IUserCache userCache;
	private readonly IAuthenticationCache authenticationCache;

	/**
	 * <summary>
	 * Instantiates a new <see cref="LoginUserModelUseCase"/>
	 * instance.
	 * </summary>
	 * <param name="httpClient">The <see cref="ApiHttpClient"/> to use.</param>
	 * <param name="userCache">The user cache to use.</param>
	 * <param name="authenticationCache">The authentication cache to use.</param>
	 */
	public LoginUserModelUseCase(HttpClient httpClient,
								 IUserCache userCache,
								 IAuthenticationCache authenticationCache)
	{
		this.httpClient = httpClient;
		this.userCache = userCache;
		this.authenticationCache = authenticationCache;
	}

	public override async Task<bool> ExecuteAsync(UserCredentialsModel input)
	{
		var baseUri = new Uri("http://localhost:5103/api/v1/");
		var endpointUri = new Uri("user/login", UriKind.Relative);
		var uri = new Uri(baseUri, endpointUri);
		var request = new LoginUserEndpointRequest
		{
			Email = input.Email,
			Password = input.Password
		};

		var jsonContent = JsonContent.Create(request);
		var response = await httpClient.PostAsync(uri, jsonContent);

		if (!response.IsSuccessStatusCode)
		{
			return false;
		}

		var content = await response.Content.ReadFromJsonAsync<LoginUserEndpointResponse>();

		if (content == null)
		{
			// TODO: Log an error message.

			return false;
		}

		// TODO: Refactor this into an "Update" method like the user cache.
		authenticationCache.Token = content.Token;
		var userModel = new UserModel
		{
			UserId = content.User.UserId,
			Email = content.User.Email,
			FirstName = content.User.FirstName,
			LastName = content.User.LastName,
			IsAdmin = content.User.IsAdmin,
			IsActive = true
		};

		userCache.Update(userModel);

		return true;
	}
}