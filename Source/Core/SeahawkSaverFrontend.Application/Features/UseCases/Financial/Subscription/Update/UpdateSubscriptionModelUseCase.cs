namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Subscription.Update;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Abstractions.UseCases.Financial;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Subscription.Update.DTOs;
using SeahawkSaverFrontend.Application.Utilities;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A use case for updating a subscription for the authenticated user in the backend API.
 * </summary>
 */
public sealed class UpdateSubscriptionModelUseCase : UpdateFinancialModelUseCase<SubscriptionModel, UpdateSubscriptionEndpointRequest>
{
	/**
	 * <summary>
	 * Instantiates a new <see cref="UpdateSubscriptionModelUseCase"/> instance.
	 * </summary>
	 * <param name="httpClient">The <see cref="ApiHttpClient"/> to use.</param>
	 * <param name="userCache">The user cache.</param>
	 * <param name="financialModelCache">The financial model cache.</param>
	 */
	public UpdateSubscriptionModelUseCase(ApiHttpClient httpClient,
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

	protected override UpdateSubscriptionEndpointRequest MapRequest(SubscriptionModel financialModel)
	{
		if (financialModel.DateTime == null)
		{
			throw new InvalidOperationException("The date must be provided.");
		}

		return new UpdateSubscriptionEndpointRequest
		{
			Subscription = new UpdateSubscriptionEndpointSubscriptionRequest
			{
				SubscriptionId = financialModel.Id,
				Amount = financialModel.Amount,
				DateTime = financialModel.DateTime.Value
			}
		};
	}
}