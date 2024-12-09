namespace SeahawkSaverFrontend.Domain.Models.Financial;
/**
 * <summary>
 * A financial model containing expense related data.
 * </summary>
 */
public class ExpenseModel : FinancialModel
{
	public decimal Amount { get; set; }
	public DateTime? DateTime { get; set; }
}