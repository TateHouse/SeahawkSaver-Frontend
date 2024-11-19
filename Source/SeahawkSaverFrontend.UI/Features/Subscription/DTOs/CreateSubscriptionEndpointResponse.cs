namespace SeahawkSaverFrontend.UI.Features.Subscription.DTOs;
public sealed record CreateSubscriptionEndpointResponse
{
	public required Guid SubscriptionId { get; init; }
}