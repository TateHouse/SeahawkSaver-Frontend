namespace SeahawkSaverFrontend.Application.Abstractions.UseCases.Financial;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Utilities;
using SeahawkSaverFrontend.Domain.Models.Financial;
using System.Net.Http.Json;

/**
 * <summary>
 * An abstract base class for all financial models to update an existing database entity in the backend API.
 * </summary>
 * <typeparam name="TFinancialModel">The type of the financial model.</typeparam>
 * <typeparam name="TEndpointRequest">The type of the request provided to the endpoint.</typeparam>
 */
public abstract class UpdateFinancialModelUseCase<TFinancialModel, TEndpointRequest> : UseCase<TFinancialModel, bool>
	where TFinancialModel : FinancialModel
	where TEndpointRequest : class
{
	private readonly ApiHttpClient httpClient;
	private readonly IUserCache userCache;
	private readonly IFinancialModelCache<TFinancialModel> financialModelCache;

	/**
	 * <summary>
	 * Instantiates a new <see cref="UpdateFinancialModelUseCase{TFinancialModel,TEndpointRequest}"/> instance.
	 * </summary>
	 * <param name="httpClient">The <see cref="ApiHttpClient"/> to use.</param>
	 * <param name="userCache">The user cache.</param>
	 * <param name="financialModelCache">The financial model cache.</param>
	 */
	protected UpdateFinancialModelUseCase(ApiHttpClient httpClient,
										  IUserCache userCache,
										  IFinancialModelCache<TFinancialModel> financialModelCache)
	{
		this.httpClient = httpClient;
		this.userCache = userCache;
		this.financialModelCache = financialModelCache;
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
	 * Gets the query parameters for the endpoint.
	 * </summary>
	 * <returns>A string representation of the query parameters without the initial '?' character.</returns>
	 */
	protected abstract string GetEndpointQueryParameters(TFinancialModel financialModel);

	/**
	 * <summary>
	 * Maps the <paramref name="financialModel"/> to the <typeparamref name="TEndpointRequest"/>.
	 * </summary>
	 * <param name="financialModel">The model to map.</param>
	 * <returns>The data contained in the request for the endpoint.</returns>
	 */
	protected abstract TEndpointRequest MapRequest(TFinancialModel financialModel);

	public override async Task<bool> ExecuteAsync(TFinancialModel input)
	{
		var baseUri = new Uri("http://localhost:5103/api/v1/");
		var endpointRelativePath = GetEndpointRelativePath();
		var endpointQueryParameters = GetEndpointQueryParameters(input);
		var endpointUri = new Uri($"{endpointRelativePath}/{userCache.User.UserId}?{endpointQueryParameters}", UriKind.Relative);
		var uri = new Uri(baseUri, endpointUri);
		var request = MapRequest(input);
		var jsonContent = JsonContent.Create(request);
		var response = await httpClient.PutAsync(uri, jsonContent);

		if (!response.IsSuccessStatusCode)
		{
			// TODO: Log an error message.

			return false;
		}

		financialModelCache.Update(input);

		return true;
	}
}