namespace SeahawkSaverFrontend.UI.Features.Subscription.Manage.DTOs;
public sealed record CreateSubscriptionEndpointResponse
{
	public required Guid SubscriptionId { get; init; }
}