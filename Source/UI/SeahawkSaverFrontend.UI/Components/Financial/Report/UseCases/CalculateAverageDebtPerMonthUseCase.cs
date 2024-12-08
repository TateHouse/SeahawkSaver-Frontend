namespace SeahawkSaverFrontend.UI.Components.Financial.Report.UseCases;
using SeahawkSaverFrontend.UI.Components.Financial.Report.DTOs;

/**
 * <summary>
 * A use case for calculating the average debt per month.
 * </summary>
 */
public sealed class CalculateAverageDebtPerMonthUseCase : CalculateAverageFinancialModelPerMonthUseCase
{
	protected override decimal GetMonthTotalAmount(FinancialModelMonthTotal financialModelMonthTotal)
	{
		return financialModelMonthTotal.DebtModelTotal.Amount;
	}
}