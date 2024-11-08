namespace SeahawkSaverFrontend.UI.Features.Subscription.Manage.DTOs;
public sealed record ListSubscriptionEndpointResponse
{
	public required IReadOnlyList<ListSubscriptionEndpointSubscriptionResponse> Subscriptions { get; init; }
}