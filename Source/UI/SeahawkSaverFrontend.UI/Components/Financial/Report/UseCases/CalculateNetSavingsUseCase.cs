namespace SeahawkSaverFrontend.UI.Components.Financial.Report.UseCases;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Abstractions.UseCases;
using SeahawkSaverFrontend.Domain.Models.Financial;
using SeahawkSaverFrontend.Domain.Models.Utilities;
using SeahawkSaverFrontend.UI.Components.Financial.Report.DTOs;
using SeahawkSaverFrontend.UI.Components.Financial.Report.Utilities;

/**
 * <summary>
 * A use case for calculating the net savings for models within a specific date range.
 * </summary>
 */
public sealed class CalculateNetSavingsUseCase : UseCase<DateRangeModel, NetSavings>
{
	private readonly IFinancialModelCache<DebtModel> debtModelCache;
	private readonly IFinancialModelCache<ExpenseModel> expenseModelCache;
	private readonly IFinancialModelCache<IncomeModel> incomeModelCache;
	private readonly IFinancialModelCache<SavingModel> savingModelCache;
	private readonly IFinancialModelCache<SubscriptionModel> subscriptionModelCache;

	/**
	 * <summary>
	 * Instantiates a new <see cref="CalculateNetSavingsUseCase"/> instance.
	 * </summary>
	 * <param name="debtModelCache">The debt cache to use.</param>
	 * <param name="expenseModelCache">The expense cache to use.</param>
	 * <param name="incomeModelCache">The income cache to use.</param>
	 * <param name="savingModelCache">The saving cache to use.</param>
	 * <param name="subscriptionModelCache">The subscription cache to use.</param>
	 */
	public CalculateNetSavingsUseCase(IFinancialModelCache<DebtModel> debtModelCache,
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

	public override Task<NetSavings> ExecuteAsync(DateRangeModel input)
	{
		var financialModelTotals = new Dictionary<FinancialModelType, FinancialModelTotal>
		{
			{
				FinancialModelType.Debt,
				FinancialModelTotalUtilities.GetTotal(input, debtModelCache, FinancialModelType.Debt)
			},
			{
				FinancialModelType.Expense,
				FinancialModelTotalUtilities.GetTotal(input, expenseModelCache, FinancialModelType.Expense)
			},
			{
				FinancialModelType.Income,
				FinancialModelTotalUtilities.GetTotal(input, incomeModelCache, FinancialModelType.Income)
			},
			{
				FinancialModelType.Saving,
				FinancialModelTotalUtilities.GetTotal(input, savingModelCache, FinancialModelType.Saving)
			},
			{
				FinancialModelType.Subscription,
				FinancialModelTotalUtilities.GetTotal(input, subscriptionModelCache, FinancialModelType.Subscription)
			}
		};

		var totalIncome = financialModelTotals[FinancialModelType.Income].Amount;
		var totalExpenses = financialModelTotals[FinancialModelType.Expense].Amount + financialModelTotals[FinancialModelType.Subscription].Amount;
		var savings = totalIncome - totalExpenses;
		var percentage = totalIncome > 0 ? (savings / totalIncome) : 0.0m;

		return Task.FromResult(new NetSavings
		{
			Amount = savings,
			Percentage = percentage
		});
	}
}