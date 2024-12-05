namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Subscription.List.DTOs;
/**
 * <summary>
 * A data transfer object containing the data returned by the list subscription endpoint response.
 * </summary>
 */
public sealed record ListSubscriptionEndpointResponse
{
	public required IReadOnlyList<ListSubscriptionEndpointSubscriptionResponse> Subscriptions { get; init; }
}