namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Subscription.List;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Abstractions.UseCases.Financial;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Subscription.List.DTOs;
using SeahawkSaverFrontend.Application.Utilities;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A use case for retrieving all subscriptions for the authenticated user from the backend API.
 * </summary>
 */
public sealed class ListSubscriptionModelUseCase : ListFinancialModelUseCase<SubscriptionModel, ListSubscriptionEndpointResponse>
{
	/**
	 * <summary>
	 * Instantiates a new <see cref="ListSubscriptionModelUseCase"/> instance.
	 * </summary>
	 * <param name="httpClient">The <see cref="ApiHttpClient"/> to use.</param>
	 * <param name="userCache">The user cache.</param>
	 */
	public ListSubscriptionModelUseCase(ApiHttpClient httpClient, IUserCache userCache)
		: base(httpClient, userCache)
	{

	}

	protected override string GetEndpointRelativePath()
	{
		return "subscription/list";
	}

	protected override IEnumerable<SubscriptionModel> MapResponse(ListSubscriptionEndpointResponse response)
	{
		return response.Subscriptions.Select(subscription => new SubscriptionModel
		{
			Id = subscription.SubscriptionId,
			Amount = subscription.Amount,
			DateTime = subscription.DateTime
		});
	}
}