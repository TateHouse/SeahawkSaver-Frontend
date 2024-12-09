namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Subscription.Create.DTOs;
/**
 * <summary>
 * A data transfer object containing the data returned by the create subscription endpoint response.
 * </summary>
 */
public sealed record CreateSubscriptionEndpointResponse
{
	public required Guid SubscriptionId { get; init; }
}