namespace SeahawkSaverFrontend.UI.Components.Financial.Report;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Abstractions.UseCases;
using SeahawkSaverFrontend.Domain.Models.Financial;
using SeahawkSaverFrontend.Domain.Models.Utilities;
using SeahawkSaverFrontend.UI.Components.Financial.Report.DTOs;
using SeahawkSaverFrontend.UI.Components.Financial.Report.UseCases;
using SeahawkSaverFrontend.UI.Components.Financial.Report.Utilities;

/**
 * <summary>
 * A manager for the <see cref="FinancialReportPage"/>.
 * </summary>
 */
public sealed class FinancialReportManager
{
	private readonly IUseCaseFactory useCaseFactory;
	private readonly IFinancialModelCache<DebtModel> debtModelCache;
	private readonly IFinancialModelCache<ExpenseModel> expenseModelCache;
	private readonly IFinancialModelCache<IncomeModel> incomeModelCache;
	private readonly IFinancialModelCache<SavingModel> savingModelCache;
	private readonly IFinancialModelCache<SubscriptionModel> subscriptionModelCache;

	public IEnumerable<FinancialModelTotal> FinancialModelTotals { get; private set; }
	public NetSavings NetSavings { get; private set; }

	/**
	 * <summary>
	 * Instantiates a new <see cref="FinancialReportManager"/> instance.
	 * </summary>
	 * <param name="useCaseFactory">The use case factory to use.</param>
	 * <param name="debtModelCache">The debt cache to use.</param>
	 * <param name="expenseModelCache">The expense cache to use.</param>
	 * <param name="incomeModelCache">The income cache to use.</param>
	 * <param name="savingModelCache">The saving cache to use.</param>
	 * <param name="subscriptionModelCache">The subscription cache to use.</param>
	 */
	public FinancialReportManager(IUseCaseFactory useCaseFactory,
								  IFinancialModelCache<DebtModel> debtModelCache,
								  IFinancialModelCache<ExpenseModel> expenseModelCache,
								  IFinancialModelCache<IncomeModel> incomeModelCache,
								  IFinancialModelCache<SavingModel> savingModelCache,
								  IFinancialModelCache<SubscriptionModel> subscriptionModelCache)
	{
		this.useCaseFactory = useCaseFactory;
		this.debtModelCache = debtModelCache;
		this.expenseModelCache = expenseModelCache;
		this.incomeModelCache = incomeModelCache;
		this.savingModelCache = savingModelCache;
		this.subscriptionModelCache = subscriptionModelCache;
	}

	/**
	 * <summary>
	 * Asynchronously generates the financial report.
	 * </summary>
	 * <param name="dateRangeModel">An optional date range. If null, then all models in the cache will be used.</param>
	 * <returns>A task that represents the asynchronous operation.</returns>
	 */
	public async Task GenerateAsync(DateRangeModel? dateRangeModel)
	{
		await CalculateTotals(dateRangeModel);
		await CalculateNetSavings(dateRangeModel);
	}

	/**
	 * <summary>
	 * Asynchronously calculates and caches the total for each <see cref="FinancialModel"/> within the specified date range.
	 * </summary>
	 * <param name="dateRangeModel">An optional date range. If null, then all models in the cache will be used.</param>
	 * <returns>A task that represents the asynchronous operation.</returns>
	 */
	private Task CalculateTotals(DateRangeModel? dateRangeModel)
	{
		// TODO: Refactor this into a use case.
		FinancialModelTotals = new List<FinancialModelTotal>
		{
			FinancialModelTotalUtilities.GetTotal(dateRangeModel, debtModelCache, FinancialModelType.Debt),
			FinancialModelTotalUtilities.GetTotal(dateRangeModel, expenseModelCache, FinancialModelType.Expense),
			FinancialModelTotalUtilities.GetTotal(dateRangeModel, incomeModelCache, FinancialModelType.Income),
			FinancialModelTotalUtilities.GetTotal(dateRangeModel, savingModelCache, FinancialModelType.Saving),
			FinancialModelTotalUtilities.GetTotal(dateRangeModel, subscriptionModelCache, FinancialModelType.Subscription)
		};

		return Task.CompletedTask;
	}

	/**
	 * <summary>
	 * Asynchronously calculates and caches the net savings.
	 * </summary>
	 * <param name="dateRangeModel">An optional date range. If null, then all models in the cache will be used.</param>
	 * <returns>A task that represents the asynchronous operation.</returns>
	 */
	private async Task CalculateNetSavings(DateRangeModel? dateRangeModel)
	{
		var useCase = useCaseFactory.Create<CalculateNetSavingsUseCase>();
		NetSavings = await useCase.ExecuteAsync(dateRangeModel);
	}
}