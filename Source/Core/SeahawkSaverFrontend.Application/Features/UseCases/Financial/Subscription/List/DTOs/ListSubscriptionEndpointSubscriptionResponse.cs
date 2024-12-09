namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Subscription.List.DTOs;
/**
 * <summary>
 * A data transfer object containing the debt data provided in the list subscription endpoint response.
 * </summary>
 */
public sealed record ListSubscriptionEndpointSubscriptionResponse
{
	public required Guid SubscriptionId { get; init; }
	public required decimal Amount { get; init; }
	public required DateTime DateTime { get; init; }
}