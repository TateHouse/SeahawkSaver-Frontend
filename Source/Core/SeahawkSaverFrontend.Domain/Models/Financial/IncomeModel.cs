namespace SeahawkSaverFrontend.Domain.Models.Financial;
/**
 * <summary>
 * A financial model containing income related data.
 * </summary>
 */
public class IncomeModel : FinancialModel
{
	public decimal Amount { get; set; }
	public DateTime? DateTime { get; set; }
}