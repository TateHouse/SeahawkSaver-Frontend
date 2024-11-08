namespace SeahawkSaverFrontend.UI.Features.Subscription.DTOs;
using System.ComponentModel.DataAnnotations;

public class SubscriptionModel
{
	public Guid SubscriptionId { get; set; }

	[Required]
	public decimal Amount { get; set; }

	[Required]
	public DateTime? DateTime { get; set; }
}