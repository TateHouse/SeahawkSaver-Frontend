namespace SeahawkSaverFrontend.UI.Components.Financial.Report.UseCases;
using SeahawkSaverFrontend.UI.Components.Financial.Report.DTOs;

/**
 * <summary>
 * A use case for calculating the average saving per month.
 * </summary>
 */
public sealed class CalculateAverageSavingPerMonthUseCase : CalculateAverageFinancialModelPerMonthUseCase
{
	protected override decimal GetMonthTotalAmount(FinancialModelMonthTotal financialModelMonthTotal)
	{
		return financialModelMonthTotal.SavingModelTotal.Amount;
	}
}