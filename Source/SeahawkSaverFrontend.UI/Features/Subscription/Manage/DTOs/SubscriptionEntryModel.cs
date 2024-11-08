namespace SeahawkSaverFrontend.UI.Features.Subscription.Manage.DTOs;
using SeahawkSaverFrontend.UI.Features.Subscription.DTOs;

public sealed class SubscriptionEntryModel : SubscriptionModel
{
	public bool IsEditable { get; set; } = false;
	public string? ErrorMessage { get; set; }
}