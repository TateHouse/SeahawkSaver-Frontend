namespace SeahawkSaverFrontend.Application.Utilities;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Exceptions;

/**
 * <summary>
 * A wrapper class for <see cref="HttpClient"/> to use for making HTTP requests to the backend API.
 * </summary>
 */
public sealed class ApiHttpClient
{
	private readonly HttpClient httpClient;
	private readonly IAuthenticationCache authenticationCache;

	/**
	 * <summary>
	 * Instantiates a new <see cref="ApiHttpClient"/> instance.
	 * </summary>
	 * <param name="httpClient">The <see cref="HttpClient"/> to use.</param>
	 * <param name="authenticationCache">The authentication cache.</param>
	 */
	public ApiHttpClient(HttpClient httpClient, IAuthenticationCache authenticationCache)
	{
		if (!authenticationCache.IsAuthenticated())
		{
			throw new UnauthorizedException();
		}

		this.httpClient = httpClient;
		this.authenticationCache = authenticationCache;

		AddBearerHeader();
	}

	/**
	 * <summary>
	 * Asynchronously sends a GET request to the specified <paramref name="uri"/>.
	 * </summary>
	 * <param name="uri">The uri to send the GET request to.</param>
	 * <returns>A task that represents the asynchronous operation, and it contains the endpoint's response.</returns>
	 */
	public async Task<HttpResponseMessage> GetAsync(Uri uri)
	{
		return await httpClient.GetAsync(uri);
	}

	/**
	 * <summary>
	 * Adds the Bearer authentication header to the <see cref="HttpClient"/>.
	 * </summary>
	 */
	private void AddBearerHeader()
	{
		if (httpClient.DefaultRequestHeaders.Contains("Bearer"))
		{
			return;
		}

		httpClient.DefaultRequestHeaders.Add("Bearer", authenticationCache.Token);
	}
}