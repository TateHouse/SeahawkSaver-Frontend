namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Subscription.Update.DTOs;
/**
 * <summary>
 * A data transfer object containing the data provided in the update subscription endpoint request.
 * </summary>
 */
public class UpdateSubscriptionEndpointRequest
{
	public required UpdateSubscriptionEndpointSubscriptionRequest Subscription { get; init; }
}