namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Subscription.Delete;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Abstractions.UseCases.Financial;
using SeahawkSaverFrontend.Application.Utilities;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A use case for deleting a subscription for the authenticated user in the backend API.
 * </summary>
 */
public sealed class DeleteSubscriptionModelUseCase : DeleteFinancialModelUseCase<SubscriptionModel>
{
	/**
	 * <summary>
	 * Instantiates a new <see cref="DeleteSubscriptionModelUseCase"/> instance.
	 * </summary>
	 * <param name="httpClient">The <see cref="ApiHttpClient"/> to use.</param>
	 * <param name="userCache">The user cache.</param>
	 * <param name="financialModelCache">The financial model cache.</param>
	 */
	public DeleteSubscriptionModelUseCase(ApiHttpClient httpClient,
										  IUserCache userCache,
										  IFinancialModelCache<SubscriptionModel> financialModelCache)
		: base(httpClient, userCache, financialModelCache)
	{

	}

	protected override string GetEndpointRelativePath()
	{
		return "subscription";
	}

	protected override string GetEndpointQueryParameters(SubscriptionModel financialModel)
	{
		return $"subscriptionId={financialModel.Id}";
	}
}