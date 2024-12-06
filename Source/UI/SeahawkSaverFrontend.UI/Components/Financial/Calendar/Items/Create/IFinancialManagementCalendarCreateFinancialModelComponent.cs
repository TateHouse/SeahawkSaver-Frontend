namespace SeahawkSaverFrontend.UI.Components.Financial.Calendar.Items.Create;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * An interface for the component which creates a <see cref="FinancialModel"/>.
 * </summary>
 */
public interface IFinancialManagementCalendarCreateFinancialModelComponent
{
	/**
	 * <summary>
	 * Creates and sends the <see cref="FinancialModel"/> to the backend if the form properties are valid.
	 * </summary>
	 * <returns>A task that represents the asynchronous operation, and it contains a bool which indicates whether the
	 * creation was successful.</returns>
	 */
	public Task<bool> Create();
}