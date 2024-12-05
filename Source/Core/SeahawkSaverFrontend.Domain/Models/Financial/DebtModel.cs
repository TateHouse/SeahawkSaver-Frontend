namespace SeahawkSaverFrontend.Domain.Models.Financial;
/**
 * <summary>
 * A financial model containing debt related data.
 * </summary>
 */
public class DebtModel : FinancialModel
{
	public decimal Amount { get; set; }
	public DateTime? DateTime { get; set; }
}