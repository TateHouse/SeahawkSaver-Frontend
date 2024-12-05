namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Subscription.Create.DTOs;
/**
 * <summary>
 * A data transfer object containing the subscription data provided in the create subscription endpoint request.
 * </summary>
 */
public sealed record CreateSubscriptionEndpointSubscriptionRequest
{
	public required decimal Amount { get; init; }
	public required DateTime DateTime { get; init; }
}