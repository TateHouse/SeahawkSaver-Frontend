namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Subscription.Update.DTOs;
/**
 * <summary>
 * A data transfer object containing the subscription data provided in the update subscription endpoint request.
 * </summary>
 */
public sealed record UpdateSubscriptionEndpointSubscriptionRequest
{
	public required Guid SubscriptionId { get; init; }
	public required decimal Amount { get; init; }
	public required DateTime DateTime { get; init; }
}