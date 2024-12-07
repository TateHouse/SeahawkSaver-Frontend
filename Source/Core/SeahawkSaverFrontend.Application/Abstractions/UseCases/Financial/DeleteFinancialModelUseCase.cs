namespace SeahawkSaverFrontend.Application.Abstractions.UseCases.Financial;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Utilities;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * An abstract base class for all financial models to delete an existing database entity in the backend API.
 * </summary>
 * <typeparam name="TFinancialModel">The type of the financial model.</typeparam>
 */
public abstract class DeleteFinancialModelUseCase<TFinancialModel> : UseCase<TFinancialModel, bool>
	where TFinancialModel : FinancialModel
{
	private readonly ApiHttpClient httpClient;
	private readonly IUserCache userCache;

	/**
	 * <summary>
	 * Instantiates a new <see cref="DeleteFinancialModelUseCase{TFinancialModel}"/> instance.
	 * </summary>
	 * <param name="httpClient">The <see cref="ApiHttpClient"/> to use.</param>
	 * <param name="userCache">The user cache.</param>
	 */
	protected DeleteFinancialModelUseCase(ApiHttpClient httpClient, IUserCache userCache)
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
	 * Gets the query parameters for the endpoint.
	 * </summary>
	 * <returns>A string representation of the query parameters without the initial '?' character.</returns>
	 */
	protected abstract string GetEndpointQueryParameters(TFinancialModel financialModel);

	public override async Task<bool> ExecuteAsync(TFinancialModel input)
	{
		var baseUri = new Uri("http://localhost:5103/api/v1/");
		var endpointRelativePath = GetEndpointRelativePath();
		var endpointQueryParameters = GetEndpointQueryParameters(input);
		var endpointUri = new Uri($"{endpointRelativePath}/{userCache.User.UserId}?{endpointQueryParameters}", UriKind.Relative);
		var uri = new Uri(baseUri, endpointUri);
		var response = await httpClient.DeleteAsync(uri);

		if (!response.IsSuccessStatusCode)
		{
			// TODO: Log an error message.

			return false;
		}

		return true;
	}
}