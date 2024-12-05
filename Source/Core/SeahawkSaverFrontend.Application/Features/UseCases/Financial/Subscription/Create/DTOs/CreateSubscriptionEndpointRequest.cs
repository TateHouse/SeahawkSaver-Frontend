namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Subscription.Create.DTOs;
/**
 * <summary>
 * A data transfer object containing the data provided in the create subscription endpoint request.
 * </summary>
 */
public sealed record CreateSubscriptionEndpointRequest
{
	public required CreateSubscriptionEndpointSubscriptionRequest Subscription { get; init; }
}