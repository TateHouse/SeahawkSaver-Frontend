namespace SeahawkSaverFrontend.Application.Abstractions.UseCases.Financial;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Utilities;
using SeahawkSaverFrontend.Domain.Models.Financial;
using System.Net.Http.Json;

/**
 * <summary>
 * An abstract base class for all financial models to retrieve all data of that financial model type from the backend
 * API.
 * </summary>
 * <typeparam name="TFinancialModel">The type of the financial model.</typeparam>
 * <typeparam name="TEndpointResponse">The type of the response returned by the endpoint.</typeparam>
 */
public abstract class ListFinancialModelUseCase<TFinancialModel, TEndpointResponse> : UseCase<object?, IEnumerable<TFinancialModel>>
	where TFinancialModel : FinancialModel
	where TEndpointResponse : class
{
	private readonly ApiHttpClient httpClient;
	private readonly IUserCache userCache;

	/**
	 * <summary>
	 * Instantiates a new <see cref="ListFinancialModelUseCase{TFinancialModel,TEndpointResponse}"/> instance.
	 * </summary>
	 * <param name="httpClient">The <see cref="ApiHttpClient"/> to use.</param>
	 * <param name="userCache">The user cache.</param>
	 */
	protected ListFinancialModelUseCase(ApiHttpClient httpClient, IUserCache userCache)
	{
		this.httpClient = httpClient;
		this.userCache = userCache;
	}

	/**
	 * <summary>
	 * Gets the specific endpoint's relative path without including the user's id.
	 * </summary>
	 * <returns>A string representation of the endpoint's relative path without the user's id.</returns>
	 */
	protected abstract string GetEndpointRelativePath();

	/**
	 * <summary>
	 * Maps the <typeparamref name="TEndpointResponse"/> to the use case's output.
	 * </summary>
	 * <returns>An enumerable of <typeparamref name="TFinancialModel"/>.</returns>
	 */
	protected abstract IEnumerable<TFinancialModel> MapResponse(TEndpointResponse response);

	public override async Task<IEnumerable<TFinancialModel>> ExecuteAsync(object? input)
	{
		// TODO: Refactor the hardcoded URI into a configuration file.
		var baseUri = new Uri("http://localhost:5103/api/v1/");
		var endpointRelativePath = GetEndpointRelativePath();
		var endpointUri = new Uri($"{endpointRelativePath}/{userCache.User?.UserId}", UriKind.Relative);
		var uri = new Uri(baseUri, endpointUri);
		var response = await httpClient.GetAsync(uri);

		if (!response.IsSuccessStatusCode)
		{
			// TODO: Log an error message.

			return new List<TFinancialModel>();
		}

		var content = await response.Content.ReadFromJsonAsync<TEndpointResponse>();

		if (content == null)
		{
			// TODO: Log an error message.

			return new List<TFinancialModel>();
		}

		return MapResponse(content);
	}
}