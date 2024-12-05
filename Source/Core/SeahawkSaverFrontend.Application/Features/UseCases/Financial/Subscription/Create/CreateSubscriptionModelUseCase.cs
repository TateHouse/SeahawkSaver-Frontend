namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Subscription.Create;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Abstractions.UseCases.Financial;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Subscription.Create.DTOs;
using SeahawkSaverFrontend.Application.Utilities;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A use case for creating a subscription for the authenticated user in the backend API.
 * </summary>
 */
public sealed class CreateSubscriptionModelUseCase : CreateFinancialModelUseCase<SubscriptionModel, CreateSubscriptionEndpointRequest, CreateSubscriptionEndpointResponse>
{
	/**
	 * <summary>
	 * Instantiates a new <see cref="CreateSubscriptionModelUseCase"/> instance.
	 * </summary>
	 * <param name="httpClient">The <see cref="ApiHttpClient"/> to use.</param>
	 * <param name="userCache">The user cache.</param>
	 */
	public CreateSubscriptionModelUseCase(ApiHttpClient httpClient, IUserCache userCache)
		: base(httpClient, userCache)
	{

	}

	protected override string GetEndpointRelativePath()
	{
		return "subscription";
	}

	protected override CreateSubscriptionEndpointRequest MapRequest(SubscriptionModel financialModel)
	{
		if (financialModel.DateTime == null)
		{
			throw new InvalidOperationException("The date must be provided.");
		}

		return new CreateSubscriptionEndpointRequest
		{
			Subscription = new CreateSubscriptionEndpointSubscriptionRequest
			{
				Amount = financialModel.Amount,
				DateTime = financialModel.DateTime.Value
			}
		};
	}

	protected override void MapId(SubscriptionModel financialModel, CreateSubscriptionEndpointResponse response)
	{
		financialModel.Id = response.SubscriptionId;
	}
}