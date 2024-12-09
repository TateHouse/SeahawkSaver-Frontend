namespace SeahawkSaverFrontend.UI.Components.Financial.Report.UseCases;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Abstractions.UseCases;
using SeahawkSaverFrontend.Domain.Models.Financial;
using SeahawkSaverFrontend.Domain.Models.Utilities;
using SeahawkSaverFrontend.UI.Components.Financial.Report.DTOs;
using SeahawkSaverFrontend.UI.Components.Financial.Report.Utilities;

/**
 * <summary>
 * A use case for calculating the monthly totals.
 * </summary>
 */
public sealed class CalculateCurrentYearMonthlyTotalsUseCase : UseCase<object?, IEnumerable<FinancialModelMonthTotal>>
{
	private readonly IFinancialModelCache<DebtModel> debtModelCache;
	private readonly IFinancialModelCache<ExpenseModel> expenseModelCache;
	private readonly IFinancialModelCache<IncomeModel> incomeModelCache;
	private readonly IFinancialModelCache<SavingModel> savingModelCache;
	private readonly IFinancialModelCache<SubscriptionModel> subscriptionModelCache;

	/**
	 * <summary>
	 * Instantiates a new <see cref="CalculateCurrentYearMonthlyTotalsUseCase"/> instance.
	 * </summary>
	 * <param name="debtModelCache">The debt cache to use.</param>
	 * <param name="expenseModelCache">The expense cache to use.</param>
	 * <param name="incomeModelCache">The income cache to use.</param>
	 * <param name="savingModelCache">The saving cache to use.</param>
	 * <param name="subscriptionModelCache">The subscription cache to use.</param>
	 */
	public CalculateCurrentYearMonthlyTotalsUseCase(IFinancialModelCache<DebtModel> debtModelCache,
										 IFinancialModelCache<ExpenseModel> expenseModelCache,
										 IFinancialModelCache<IncomeModel> incomeModelCache,
										 IFinancialModelCache<SavingModel> savingModelCache,
										 IFinancialModelCache<SubscriptionModel> subscriptionModelCache)
	{
		this.debtModelCache = debtModelCache;
		this.expenseModelCache = expenseModelCache;
		this.incomeModelCache = incomeModelCache;
		this.savingModelCache = savingModelCache;
		this.subscriptionModelCache = subscriptionModelCache;
	}

	public override Task<IEnumerable<FinancialModelMonthTotal>> ExecuteAsync(object? input)
	{
		const int monthCount = 12;
		var financialModelMonthTotals = new List<FinancialModelMonthTotal>();

		for (var monthIndex = 0; monthIndex < monthCount; ++monthIndex)
		{
			var firstDayOfMonth = new DateTime(DateTime.Today.Year, monthIndex + 1, 1);
			var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
			var currentMonthRange = new DateRangeModel
			{
				Start = firstDayOfMonth,
				End = lastDayOfMonth
			};

			var financialModelTotals = new List<FinancialModelTotal>
			{
				FinancialModelTotalUtilities.GetTotal(currentMonthRange, debtModelCache, FinancialModelType.Debt),
				FinancialModelTotalUtilities.GetTotal(currentMonthRange, expenseModelCache, FinancialModelType.Expense),
				FinancialModelTotalUtilities.GetTotal(currentMonthRange, incomeModelCache, FinancialModelType.Income),
				FinancialModelTotalUtilities.GetTotal(currentMonthRange, savingModelCache, FinancialModelType.Saving),
				FinancialModelTotalUtilities.GetTotal(currentMonthRange, subscriptionModelCache, FinancialModelType.Subscription)
			};

			var financialModelMonthTotal = new FinancialModelMonthTotal
			{
				MonthIndex = monthIndex,
				DebtModelTotal = financialModelTotals[0],
				ExpenseModelTotal = financialModelTotals[1],
				IncomeModelTotal = financialModelTotals[2],
				SavingModelTotal = financialModelTotals[3],
				SubscriptionModelTotal = financialModelTotals[4]
			};

			financialModelMonthTotals.Add(financialModelMonthTotal);
		}

		return Task.FromResult(financialModelMonthTotals.AsEnumerable());
	}
}