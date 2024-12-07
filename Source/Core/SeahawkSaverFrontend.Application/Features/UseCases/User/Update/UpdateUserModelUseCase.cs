namespace SeahawkSaverFrontend.Application.Features.UseCases.User.Update;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Abstractions.UseCases;
using SeahawkSaverFrontend.Application.Features.UseCases.User.Update.DTOs;
using SeahawkSaverFrontend.Application.Utilities;
using SeahawkSaverFrontend.Domain.Models.User;
using System.Net.Http.Json;

/**
 * <summary>
 * A use case for updating the authenticated user in the backend API.
 * </summary>
 */
public sealed class UpdateUserModelUseCase : UseCase<UserModel, bool>
{
	private readonly ApiHttpClient httpClient;
	private readonly IUserCache userCache;

	/**
	 * <summary>
	 * Instantiates a new <see cref="UpdateUserModelUseCase"/> instance.
	 * </summary>
	 * <param name="httpClient">The <see cref="ApiHttpClient"/> to use.</param>
	 * <param name="userCache">The user cache.</param>
	 */
	public UpdateUserModelUseCase(ApiHttpClient httpClient, IUserCache userCache)
	{
		this.httpClient = httpClient;
		this.userCache = userCache;
	}

	public override async Task<bool> ExecuteAsync(UserModel input)
	{
		var baseUri = new Uri("http://localhost:5103/api/v1/");
		var endpointRelativePath = "user";
		var endpointUri = new Uri($"{endpointRelativePath}/{input.UserId}", UriKind.Relative);
		var uri = new Uri(baseUri, endpointUri);
		var request = MapRequest(input);
		var jsonContent = JsonContent.Create(request);
		var response = await httpClient.PutAsync(uri, jsonContent);

		if (!response.IsSuccessStatusCode)
		{
			// TODO: Log an error message.

			return false;
		}

		if (userCache.User.UserId == input.UserId)
		{
			userCache.Update(input);
		}

		return true;
	}

	/**
	 * <summary>
	 * Maps the <see cref="UserModel"/> to the <see cref="UpdateUserEndpointRequest"/>.
	 * </summary>
	 * <param name="userModel">The model to map.</param>
	 * <returns>The data contained in the request for the endpoint.</returns>
	 */
	private UpdateUserEndpointRequest MapRequest(UserModel userModel)
	{
		return new UpdateUserEndpointRequest
		{
			User = new UpdateUserEndpointUserRequest
			{
				Email = userModel.Email,
				FirstName = userModel.FirstName,
				LastName = userModel.LastName,
				IsActive = userModel.IsActive
			}
		};
	}
}