namespace SeahawkSaverFrontend.UI.Components.Financial.Calendar;
using SeahawkSaverFrontend.Domain.Models.Financial;
using SeahawkSaverFrontend.UI.Components.Financial.Calendar.Items;

/**
 * <summary>
 * A manager for the <see cref="FinancialManagementCalendarComponent"/> items.
 * </summary>
 */
public sealed class FinancialManagementCalendarItemManager
{
	private readonly List<IFinancialModelCalendarItemFactory<FinancialModel>> financialModelCalendarItemFactories;

	public IList<FinancialModelCalendarItem> CalendarItems { get; } = new List<FinancialModelCalendarItem>();

	/**
	 * <summary>
	 * Instantiates a new <see cref="FinancialManagementCalendarItemManager"/> instance.
	 * </summary>
	 * <param name="debtModelCalendarItemFactory">The factory for debt items.</param>
	 * <param name="expenseModelCalendarItemFactory">The factory for expense items.</param>
	 * <param name="incomeModelCalendarItemFactory">The factory for income items.</param>
	 * <param name="savingModelCalendarItemFactory">The factory for saving items.</param>
	 * <param name="subscriptionModelItemFactory">The factory for subscription items.</param>
	 */
	public FinancialManagementCalendarItemManager(IFinancialModelCalendarItemFactory<DebtModel> debtModelCalendarItemFactory,
												  IFinancialModelCalendarItemFactory<ExpenseModel> expenseModelCalendarItemFactory,
												  IFinancialModelCalendarItemFactory<IncomeModel> incomeModelCalendarItemFactory,
												  IFinancialModelCalendarItemFactory<SavingModel> savingModelCalendarItemFactory,
												  IFinancialModelCalendarItemFactory<SubscriptionModel> subscriptionModelItemFactory)
	{
		financialModelCalendarItemFactories = new List<IFinancialModelCalendarItemFactory<FinancialModel>>
		{
			debtModelCalendarItemFactory,
			expenseModelCalendarItemFactory,
			incomeModelCalendarItemFactory,
			savingModelCalendarItemFactory,
			subscriptionModelItemFactory,
		};
	}

	/**
	 * <summary>
	 * Clears the calendar and then creates the <see cref="FinancialModelCalendarItem"/> instances.
	 * </summary>
	 */
	public void CreateFinancialModelCalendarItems()
	{
		CalendarItems.Clear();

		foreach (var factory in financialModelCalendarItemFactories)
		{
			var financialModelCalendarItems = factory.Create();

			foreach (var calendarItem in financialModelCalendarItems)
			{
				CalendarItems.Add(calendarItem);
			}
		}
	}
}