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

	public event Action? OnStateChanged;
	public IEnumerable<FinancialModelTotal> FinancialModelOverallTotals { get; private set; }
	public IEnumerable<FinancialModelMonthTotal> FinancialModelCurrentYearMonthTotals { get; private set; }
	public AverageFinancialModelsPerMonth AverageFinancialModelsPerMonth { get; private set; }
	public NetSavings NetSavings { get; private set; }
	public CashFlowFilteredFinancialModelMonthTotals CashFlowFilteredFinancialModelMonthTotals { get; private set; }

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
	 * Notifies all subscribers of the <see cref="OnStateChanged"/> event.
	 * </summary>
	 */
	public void NotifyStateChanged()
	{
		OnStateChanged?.Invoke();
	}

	/**
	 * <summary>
	 * Asynchronously generates the financial report.
	 * </summary>
	 * <param name="dateRangeModel">An optional date range. If null, then all models in the cache will be used.</param>
	 * <returns>A task that represents the asynchronous operation.</returns>
	 */
	public async Task GenerateAsync(DateRangeModel dateRangeModel)
	{
		await CalculateOverallTotalsAsync(dateRangeModel);
		await CalculateCurrentYearMonthTotalsAsync();
		await GetCurrentYearMonthsByCashFlow(FinancialModelCurrentYearMonthTotals);
		await CalculateAverageFinancialModelPerMonthAsync();
		await CalculateNetSavingsAsync(dateRangeModel);
	}

	/**
	 * <summary>
	 * Asynchronously calculates and caches the total for each <see cref="FinancialModel"/> within the specified date
	 * range.
	 * </summary>
	 * <param name="dateRangeModel">The date range.</param>
	 * <returns>A task that represents the asynchronous operation.</returns>
	 */
	private Task CalculateOverallTotalsAsync(DateRangeModel? dateRangeModel)
	{
		// TODO: Refactor this into a use case.
		FinancialModelOverallTotals = new List<FinancialModelTotal>
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
	 * Asynchronously calculates and caches the total for each <see cref="FinancialModel"/> for each month for the past
	 * year.
	 * </summary>
	 * <returns>A task that represents the asynchronous operation.</returns>
	 */
	private async Task CalculateCurrentYearMonthTotalsAsync()
	{
		var useCase = useCaseFactory.Create<CalculateCurrentYearMonthlyTotalsUseCase>();
		FinancialModelCurrentYearMonthTotals = await useCase.ExecuteAsync(null);
	}

	/**
	 * <summary>
	 * Asynchronously gets and caches the months in the current year for each type of cash flow.
	 * </summary>
	 * <param name="financialModelMonthTotals">The financial model month totals for the current year.</param>
	 * <returns>A task that represents the asynchronous operation.</returns>
	 */
	private async Task GetCurrentYearMonthsByCashFlow(IEnumerable<FinancialModelMonthTotal> financialModelMonthTotals)
	{
		var modelMonthTotals = financialModelMonthTotals.ToList();
		List<FinancialModelMonthTotal> monthsBreakingEven = null!;
		List<FinancialModelMonthTotal> monthsInTheBlack = null!;
		List<FinancialModelMonthTotal> monthsInTheRed = null!;

		{
			var useCase = useCaseFactory.Create<FilterCurrentYearMonthsByBreakingEven>();
			monthsBreakingEven = (await useCase.ExecuteAsync(modelMonthTotals)).ToList();
		}

		{
			var useCase = useCaseFactory.Create<FilterCurrentYearMonthsByInTheBlackUseCase>();
			monthsInTheBlack = (await useCase.ExecuteAsync(modelMonthTotals)).ToList();
		}

		{
			var useCase = useCaseFactory.Create<FilterCurrentYearMonthsByInTheRedUseCase>();
			monthsInTheRed = (await useCase.ExecuteAsync(modelMonthTotals)).ToList();
		}

		CashFlowFilteredFinancialModelMonthTotals = new CashFlowFilteredFinancialModelMonthTotals
		{
			MonthsBreakingEvent = monthsBreakingEven,
			MonthsInTheBlack = monthsInTheBlack,
			MonthsInTheRed = monthsInTheRed
		};
	}

	/**
	 * <summary>
	 * Asynchronously calculates and caches the average monthly totals for the current year.
	 * </summary>
	 */
	private async Task CalculateAverageFinancialModelPerMonthAsync()
	{
		var builder = new AverageFinancialModelsPerMonthBuilder(useCaseFactory, FinancialModelCurrentYearMonthTotals);
		AverageFinancialModelsPerMonth = builder.WithAverageDebt()
												.WithAverageExpense()
												.WithAverageIncome()
												.WithAverageSaving()
												.WithAverageSubscription()
												.Build();
	}

	/**
	 * <summary>
	 * Asynchronously calculates and caches the net savings.
	 * </summary>
	 * <param name="dateRangeModel">The date range.</param>
	 * <returns>A task that represents the asynchronous operation.</returns>
	 */
	private async Task CalculateNetSavingsAsync(DateRangeModel dateRangeModel)
	{
		var useCase = useCaseFactory.Create<CalculateNetSavingsUseCase>();
		NetSavings = await useCase.ExecuteAsync(dateRangeModel);
	}

}