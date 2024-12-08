namespace SeahawkSaverFrontend.UI.Components.Financial.Report.DTOs;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A data transfer object containing the average amount for each <see cref="FinancialModel"/> for each month in the
 * current year.
 * </summary>
 */
public sealed record AverageFinancialModelsPerMonth
{
	public required decimal AverageDebtAmount { get; init; }
	public required decimal AverageExpenseAmount { get; init; }
	public required decimal AverageIncomeAmount { get; init; }
	public required decimal AverageSavingAmount { get; init; }
	public required decimal AverageSubscriptionAmount { get; init; }
}