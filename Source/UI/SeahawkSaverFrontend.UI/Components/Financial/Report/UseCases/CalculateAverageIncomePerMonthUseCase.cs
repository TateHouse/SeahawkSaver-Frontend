namespace SeahawkSaverFrontend.UI.Components.Financial.Report.UseCases;
using SeahawkSaverFrontend.UI.Components.Financial.Report.DTOs;

/**
 * <summary>
 * A use case for calculating the average income per month.
 * </summary>
 */
public sealed class CalculateAverageIncomePerMonthUseCase : CalculateAverageFinancialModelPerMonthUseCase
{
	protected override decimal GetMonthTotalAmount(FinancialModelMonthTotal financialModelMonthTotal)
	{
		return financialModelMonthTotal.IncomeModelTotal.Amount;
	}
}