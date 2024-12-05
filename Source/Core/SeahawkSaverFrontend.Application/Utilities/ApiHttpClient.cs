namespace SeahawkSaverFrontend.Application.Utilities;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Exceptions;
using System.Net.Http.Json;

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
	 * Asynchronously sends a POST request to the specified <paramref name="uri"/> with the provided content.
	 * </summary>
	 * <param name="uri">The uri to send the GET request to.</param>
	 * <param name="request">The content to provide in the request.</param>
	 * <returns>A task that represents the asynchronous operation, and it contains the endpoint's response.</returns>
	 */
	public async Task<HttpResponseMessage> PostAsync(Uri uri, JsonContent request)
	{
		return await httpClient.PostAsync(uri, request);
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