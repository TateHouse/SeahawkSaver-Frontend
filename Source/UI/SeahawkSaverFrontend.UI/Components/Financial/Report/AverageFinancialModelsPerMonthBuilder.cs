namespace SeahawkSaverFrontend.UI.Components.Financial.Report;
using SeahawkSaverFrontend.Application.Abstractions.UseCases;
using SeahawkSaverFrontend.UI.Components.Financial.Report.DTOs;
using SeahawkSaverFrontend.UI.Components.Financial.Report.UseCases;

/**
 * <summary>
 * A builder for <see cref="AverageFinancialModelsPerMonth"/>.
 * </summary>
 */
public sealed class AverageFinancialModelsPerMonthBuilder
{
	private readonly IUseCaseFactory useCaseFactory;
	private readonly IEnumerable<FinancialModelMonthTotal> financialModelMonthTotals;
	private decimal averageDebtAmount;
	private decimal averageExpenseAmount;
	private decimal averageIncomeAmount;
	private decimal averageSavingAmount;
	private decimal averageSubscriptionAmount;

	/**
	 * <summary>
	 * Instantiates a new <see cref="AverageFinancialModelsPerMonthBuilder"/> instance.
	 * </summary>
	 * <param name="useCaseFactory">The use case factory to use.</param>
	 * <param name="financialModelMonthTotals">The financial model month totals.</param>
	 */
	public AverageFinancialModelsPerMonthBuilder(IUseCaseFactory useCaseFactory,
												IEnumerable<FinancialModelMonthTotal> financialModelMonthTotals)
	{
		this.useCaseFactory = useCaseFactory;
		this.financialModelMonthTotals = financialModelMonthTotals;
	}

	/**
	 * <summary>
	 * Includes the average debt.
	 * </summary>
	 * <returns>A reference to this <see cref="AverageFinancialModelsPerMonthBuilder"/> instance.</returns>
	 */
	public AverageFinancialModelsPerMonthBuilder WithAverageDebt()
	{
		var useCase = useCaseFactory.Create<CalculateAverageDebtPerMonthUseCase>();
		averageDebtAmount = useCase.ExecuteAsync(financialModelMonthTotals).GetAwaiter().GetResult();

		return this;
	}

	/**
	 * <summary>
	 * Includes the average expense.
	 * </summary>
	 * <returns>A reference to this <see cref="AverageFinancialModelsPerMonthBuilder"/> instance.</returns>
	 */
	public AverageFinancialModelsPerMonthBuilder WithAverageExpense()
	{
		var useCase = useCaseFactory.Create<CalculateAverageExpensePerMonthUseCase>();
		averageExpenseAmount = useCase.ExecuteAsync(financialModelMonthTotals).GetAwaiter().GetResult();

		return this;
	}

	/**
	 * <summary>
	 * Includes the average income.
	 * </summary>
	 * <returns>A reference to this <see cref="AverageFinancialModelsPerMonthBuilder"/> instance.</returns>
	 */
	public AverageFinancialModelsPerMonthBuilder WithAverageIncome()
	{
		var useCase = useCaseFactory.Create<CalculateAverageIncomePerMonthUseCase>();
		averageIncomeAmount = useCase.ExecuteAsync(financialModelMonthTotals).GetAwaiter().GetResult();

		return this;
	}

	/**
	 * <summary>
	 * Includes the average saving.
	 * </summary>
	 * <returns>A reference to this <see cref="AverageFinancialModelsPerMonthBuilder"/> instance.</returns>
	 */
	public AverageFinancialModelsPerMonthBuilder WithAverageSaving()
	{
		var useCase = useCaseFactory.Create<CalculateAverageSavingPerMonthUseCase>();
		averageSavingAmount = useCase.ExecuteAsync(financialModelMonthTotals).GetAwaiter().GetResult();

		return this;
	}

	/**
	 * <summary>
	 * Includes the average subscription.
	 * </summary>
	 * <returns>A reference to this <see cref="AverageFinancialModelsPerMonthBuilder"/> instance.</returns>
	 */
	public AverageFinancialModelsPerMonthBuilder WithAverageSubscription()
	{
		var useCase = useCaseFactory.Create<CalculateAverageSubscriptionPerMonthUseCase>();
		averageSubscriptionAmount = useCase.ExecuteAsync(financialModelMonthTotals).GetAwaiter().GetResult();

		return this;
	}

	/**
	 * <summary>
	 * Instantiates a <see cref="AverageFinancialModelsPerMonth"/> instance.
	 * </summary>
	 * <returns>An <see cref="AverageFinancialModelsPerMonth"/> instance.</returns>
	 */
	public AverageFinancialModelsPerMonth Build()
	{
		return new AverageFinancialModelsPerMonth
		{
			AverageDebtAmount = averageDebtAmount,
			AverageExpenseAmount = averageExpenseAmount,
			AverageIncomeAmount = averageIncomeAmount,
			AverageSavingAmount = averageSavingAmount,
			AverageSubscriptionAmount = averageSubscriptionAmount
		};
	}
}