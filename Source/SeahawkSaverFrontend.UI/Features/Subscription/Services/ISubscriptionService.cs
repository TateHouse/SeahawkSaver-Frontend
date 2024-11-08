namespace SeahawkSaverFrontend.UI.Features.Subscription.Services;
using SeahawkSaverFrontend.UI.Features.Subscription.DTOs;

public interface ISubscriptionService
{
	public Task<IEnumerable<SubscriptionModel>> GetSubscriptionsAsync();

	public Task<bool> AddSubscriptionAsync(SubscriptionModel model);

	public Task<bool> UpdateSubscriptionAsync(SubscriptionModel model);

	public Task<bool> RemoveSubscriptionAsync(SubscriptionModel model);
}