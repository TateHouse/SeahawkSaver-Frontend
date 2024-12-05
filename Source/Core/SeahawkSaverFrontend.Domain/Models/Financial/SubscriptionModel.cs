namespace SeahawkSaverFrontend.Domain.Models.Financial;
/**
 * <summary>
 * A financial model containing subscription related data.
 * </summary>
 */
public class SubscriptionModel : FinancialModel
{
	public decimal Amount { get; set; }
	public DateTime? DateTime { get; set; }
}