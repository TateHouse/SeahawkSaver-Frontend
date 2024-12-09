namespace SeahawkSaverFrontend.Domain.Models.Financial;
/**
 * <summary>
 * An abstract base class for all financial models to derive from.
 * </summary>
 */
public abstract class FinancialModel
{
	public required Guid Id { get; set; }
}