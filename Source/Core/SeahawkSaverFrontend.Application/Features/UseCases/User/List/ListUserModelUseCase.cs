namespace SeahawkSaverFrontend.Application.Features.UseCases.User.List;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Abstractions.UseCases;
using SeahawkSaverFrontend.Application.Exceptions;
using SeahawkSaverFrontend.Application.Features.UseCases.User.List.DTOs;
using SeahawkSaverFrontend.Application.Utilities;
using SeahawkSaverFrontend.Domain.Models.User;
using System.Net.Http.Json;

/**
 * <summary>
 * A use case for retrieving all users from the backend API.
 * </summary>
 */
public sealed class ListUserModelUseCase : UseCase<object?, IEnumerable<UserModel>>
{
	private readonly ApiHttpClient httpClient;
	private readonly IUserCache userCache;

	/**
	 * <summary>
	 * Instantiates a new <see cref="ListUserModelUseCase"/> instance.
	 * </summary>
	 */
	public ListUserModelUseCase(ApiHttpClient httpClient, IUserCache userCache)
	{
		this.httpClient = httpClient;
		this.userCache = userCache;
	}

	public override async Task<IEnumerable<UserModel>> ExecuteAsync(object? input)
	{
		if (!userCache.User.IsAdmin)
		{
			throw new UnauthorizedException();
		}

		// TODO: Refactor the hardcoded URI into a configuration file.
		var baseUri = new Uri("http://localhost:5103/api/v1/");
		var endpointUri = new Uri("user/list", UriKind.Relative);
		var uri = new Uri(baseUri, endpointUri);
		var response = await httpClient.GetAsync(uri);

		if (!response.IsSuccessStatusCode)
		{
			// TODO: Log an error message.

			return new List<UserModel>();
		}

		var content = await response.Content.ReadFromJsonAsync<ListUserEndpointResponse>();

		if (content == null)
		{
			// TODO: Log an error message.

			return new List<UserModel>();
		}

		return MapResponse(content);
	}

	/**
	 * <summary>
	 * Maps the <see cref="ListUserEndpointResponse"/> to the use case's output.
	 * </summary>
	 * <returns>An enumerable of <see cref="UserModel"/>.</returns>
	 */
	private static IEnumerable<UserModel> MapResponse(ListUserEndpointResponse response)
	{
		return response.Users.Select(user => new UserModel
					   {
						   UserId = user.UserId,
						   Email = user.Email,
						   FirstName = user.FirstName,
						   LastName = user.LastName,
						   IsAdmin = user.IsAdmin,
						   IsActive = user.IsActive
					   })
					   .ToList();
	}
}