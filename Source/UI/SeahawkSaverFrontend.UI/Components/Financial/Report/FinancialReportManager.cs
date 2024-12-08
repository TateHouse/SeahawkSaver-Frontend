namespace SeahawkSaverFrontend.UI.Components.Financial.Report;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Domain.Models.Financial;
using SeahawkSaverFrontend.Domain.Models.Utilities;
using SeahawkSaverFrontend.UI.Components.Financial.Report.DTOs;

/**
 * <summary>
 * A manager for the <see cref="FinancialReportPage"/>.
 * </summary>
 */
public sealed class FinancialReportManager
{
	private readonly IFinancialModelCache<DebtModel> debtModelCache;
	private readonly IFinancialModelCache<ExpenseModel> expenseModelCache;
	private readonly IFinancialModelCache<IncomeModel> incomeModelCache;
	private readonly IFinancialModelCache<SavingModel> savingModelCache;
	private readonly IFinancialModelCache<SubscriptionModel> subscriptionModelCache;

	public IEnumerable<FinancialModelTotal> FinancialModelTotals { get; private set; }

	/**
	 * <summary>
	 * Instantiates a new <see cref="FinancialReportManager"/> instance.
	 * </summary>
	 * <param name="debtModelCache">The debt cache to use.</param>
	 * <param name="expenseModelCache">The expense cache to use.</param>
	 * <param name="incomeModelCache">The income cache to use.</param>
	 * <param name="savingModelCache">The saving cache to use.</param>
	 * <param name="subscriptionModelCache">The subscription cache to use.</param>
	 */
	public FinancialReportManager(IFinancialModelCache<DebtModel> debtModelCache,
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

	/**
	 * <summary>
	 * Calculates the total for each <see cref="FinancialModel"/> within the specified date range and caches it in the
	 * <see cref="FinancialModelTotals"/> property.
	 * </summary>
	 * <param name="dateRangeModel">An optional date range. If null, then all models in the cache will be used.</param>
	 */
	public void CalculateTotals(DateRangeModel? dateRangeModel)
	{
		FinancialModelTotals = new List<FinancialModelTotal>
		{
			GetTotal(dateRangeModel, debtModelCache, FinancialModelType.Debt),
			GetTotal(dateRangeModel, expenseModelCache, FinancialModelType.Expense),
			GetTotal(dateRangeModel, incomeModelCache, FinancialModelType.Income),
			GetTotal(dateRangeModel, savingModelCache, FinancialModelType.Saving),
			GetTotal(dateRangeModel, subscriptionModelCache, FinancialModelType.Subscription)
		};
	}

	/**
	 * <summary>
	 * Gets the total for a specific <see cref="FinancialModelType"/> within the specified date range.
	 * </summary>
	 * <param name="dateRangeModel">An optional date range. If null, then all models in the cache will be used.</param>
	 * <param name="financialModelCache">The model cache to use.</param>
	 * <param name="financialModelType">The type of the financial model.</param>
	 */
	private static FinancialModelTotal GetTotal<TFinancialModel>(DateRangeModel? dateRangeModel,
																 IFinancialModelCache<TFinancialModel> financialModelCache,
																 FinancialModelType financialModelType)
		where TFinancialModel : FinancialModel
	{
		return new FinancialModelTotal
		{
			Amount = financialModelCache.GetTotal(dateRangeModel, out var count),
			Count = count,
			Type = financialModelType
		};
	}
}