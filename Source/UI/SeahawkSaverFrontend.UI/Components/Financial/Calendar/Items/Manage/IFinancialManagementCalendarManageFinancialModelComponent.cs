namespace SeahawkSaverFrontend.UI.Components.Financial.Calendar.Items.Manage;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * An interface for the component which updates or deletes a <see cref="FinancialModel"/>.
 * </summary>
 */
public interface IFinancialManagementCalendarManageFinancialModelComponent
{
	/**
	 * <summary>
	 * Asynchronously updates and sends the <see cref="FinancialModel"/> to the backend if the form properties are valid.
	 * </summary>
	 * <returns>A task that represents the asynchronous operation, and it contains a bool which indicates whether the
	 * update was successful.</returns>
	 */
	public Task<bool> UpdateAsync();

	/**
	 * <summary>
	 * Asynchronously deletes and sends the <see cref="FinancialModel"/> to the backend.
	 * </summary>
	 * <returns>As task that represents the asynchronous operation, and it contains a bool which indicates whether the
	 * deletion was successful.</returns>
	 */
	public Task<bool> DeleteAsync();
}