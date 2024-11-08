namespace SeahawkSaverFrontend.UI.Features.Subscription.Manage.DTOs;
public sealed record ListSubscriptionEndpointSubscriptionResponse
{
	public required Guid SubscriptionId { get; init; }
	public required decimal Amount { get; init; }
	public required DateTime DateTime { get; init; }
}