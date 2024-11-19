namespace SeahawkSaverFrontend.UI.Features.Subscription.DTOs;
public sealed record ListSubscriptionEndpointResponse
{
	public required IReadOnlyList<ListSubscriptionEndpointSubscriptionResponse> Subscriptions { get; init; }
}