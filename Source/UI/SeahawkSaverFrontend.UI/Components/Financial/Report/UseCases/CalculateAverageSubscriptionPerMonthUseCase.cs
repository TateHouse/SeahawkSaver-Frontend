namespace SeahawkSaverFrontend.UI.Components.Financial.Report.UseCases;
using SeahawkSaverFrontend.UI.Components.Financial.Report.DTOs;

/**
 * <summary>
 * A use case for calculating the average subscription per month.
 * </summary>
 */
public sealed class CalculateAverageSubscriptionPerMonthUseCase : CalculateAverageFinancialModelPerMonthUseCase
{
	protected override decimal GetMonthTotalAmount(FinancialModelMonthTotal financialModelMonthTotal)
	{
		return financialModelMonthTotal.SubscriptionModelTotal.Amount;
	}
}