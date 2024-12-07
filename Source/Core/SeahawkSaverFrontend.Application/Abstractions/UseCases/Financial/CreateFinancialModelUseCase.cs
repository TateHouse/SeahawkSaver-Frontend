namespace SeahawkSaverFrontend.Application.Abstractions.UseCases.Financial;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Utilities;
using SeahawkSaverFrontend.Domain.Models.Financial;
using System.Net.Http.Json;

/**
 * <summary>
 * An abstract base class for all financial models to create a new database entity in the backend API.
 * </summary>
 * <typeparam name="TFinancialModel">The type of the financial model.</typeparam>
 * <typeparam name="TEndpointRequest">The type of the request provided to the endpoint.</typeparam>
 * <typeparam name="TEndpointResponse">The type of the response returned by the endpoint.</typeparam>
 */
public abstract class CreateFinancialModelUseCase<TFinancialModel, TEndpointRequest, TEndpointResponse> : UseCase<TFinancialModel, bool>
	where TFinancialModel : FinancialModel
	where TEndpointRequest : class
	where TEndpointResponse : class
{
	private readonly ApiHttpClient httpClient;
	private readonly IUserCache userCache;

	/**
	 * <summary>
	 * Instantiates a new <see cref="CreateFinancialModelUseCase{TFinancialModel,TEndpointRequest,TEndpointResponse}"/>
	 * instance.
	 * </summary>
	 * <param name="httpClient">The <see cref="ApiHttpClient"/> to use.</param>
	 * <param name="userCache">The user cache.</param>
	 */
	protected CreateFinancialModelUseCase(ApiHttpClient httpClient, IUserCache userCache)
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
	 * Maps the <paramref name="financialModel"/> to the <typeparamref name="TEndpointRequest"/>.
	 * </summary>
	 * <param name="financialModel">The model to map.</param>
	 * <returns>The data contained in the request for the endpoint.</returns>
	 */
	protected abstract TEndpointRequest MapRequest(TFinancialModel financialModel);

	/**
	 * <summary>
	 * Maps the <typeparamref name="TEndpointResponse"/> to the id of the <see cref="FinancialModel"/>.
	 * </summary>
	 * <param name="financialModel">The model to map.</param>
	 * <param name="response">The endpoint response.</param>
	 */
	protected abstract void MapId(TFinancialModel financialModel, TEndpointResponse response);

	public override async Task<bool> ExecuteAsync(TFinancialModel input)
	{
		var baseUri = new Uri("http://localhost:5103/api/v1/");
		var endpointRelativePath = GetEndpointRelativePath();
		var endpointUri = new Uri($"{endpointRelativePath}/{userCache.User.UserId}", UriKind.Relative);
		var uri = new Uri(baseUri, endpointUri);
		var request = MapRequest(input);
		var jsonContent = JsonContent.Create(request);
		var response = await httpClient.PostAsync(uri, jsonContent);

		if (!response.IsSuccessStatusCode)
		{
			// TODO: Log an error message.

			return false;
		}

		var content = await response.Content.ReadFromJsonAsync<TEndpointResponse>();

		if (content == null)
		{
			// TODO: Log an error message.

			return false;
		}

		MapId(input, content);

		return true;
	}
}