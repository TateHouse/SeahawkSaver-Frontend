namespace SeahawkSaverFrontend.Domain.Models.Financial;
/**
 * <summary>
 * A financial model containing saving related data.
 * </summary>
 */
public class SavingModel : FinancialModel
{
	public decimal Amount { get; set; }
	public DateTime? DateTime { get; set; }
}