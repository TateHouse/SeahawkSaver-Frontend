namespace SeahawkSaverFrontend.UI.Components.Financial.Calendar.Items;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A factory to instantiate <see cref="FinancialModelCalendarItem"/> instances.
 * </summary>
 */
public interface IFinancialModelCalendarItemFactory<out TFinancialModel>
	where TFinancialModel : FinancialModel
{
	/**
	 * <summary>
	 * Creates a collection of <see cref="FinancialModelCalendarItem"/> from the corresponding
	 * <see cref="IFinancialModelCache{TFinancialModel}"/>.
	 * </summary>
	 * <returns>A collcetion of <see cref="FinancialModelCalendarItem"/>.</returns>
	 */
	public IList<FinancialModelCalendarItem> Create();
}