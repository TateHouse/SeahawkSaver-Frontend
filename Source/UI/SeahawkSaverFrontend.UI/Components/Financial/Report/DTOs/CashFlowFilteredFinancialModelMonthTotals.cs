namespace SeahawkSaverFrontend.UI.Components.Financial.Report.DTOs;
/**
 * <summary>
 * A data transfer object for different cash flow results for the months in the current year.
 * </summary>
 */
public sealed record CashFlowFilteredFinancialModelMonthTotals
{
	public required List<FinancialModelMonthTotal> MonthsBreakingEvent { get; init; }
	public required List<FinancialModelMonthTotal> MonthsInTheBlack { get; init; }
	public required List<FinancialModelMonthTotal> MonthsInTheRed { get; init; }
}