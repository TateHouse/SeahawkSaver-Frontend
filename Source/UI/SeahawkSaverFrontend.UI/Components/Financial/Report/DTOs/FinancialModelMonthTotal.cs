namespace SeahawkSaverFrontend.UI.Components.Financial.Report.DTOs;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A data transfer object containing the <see cref="FinancialModelTotal"/> for each <see cref="FinancialModelType"/> for
 * each month in the previous year.
 * </summary>
 */
public sealed record FinancialModelMonthTotal
{
	public required int MonthIndex { get; init; }
	public required FinancialModelTotal DebtModelTotal { get; init; }
	public required FinancialModelTotal ExpenseModelTotal { get; init; }
	public required FinancialModelTotal IncomeModelTotal { get; init; }
	public required FinancialModelTotal SavingModelTotal { get; init; }
	public required FinancialModelTotal SubscriptionModelTotal { get; init; }
}