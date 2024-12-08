namespace SeahawkSaverFrontend.UI.Components.Financial.Report.UseCases;
using SeahawkSaverFrontend.UI.Components.Financial.Report.DTOs;

/**
 * <summary>
 * A use case for calculating the average expense per month.
 * </summary>
 */
public sealed class CalculateAverageExpensePerMonthUseCase : CalculateAverageFinancialModelPerMonthUseCase
{
	protected override decimal GetMonthTotalAmount(FinancialModelMonthTotal financialModelMonthTotal)
	{
		return financialModelMonthTotal.ExpenseModelTotal.Amount;
	}
}