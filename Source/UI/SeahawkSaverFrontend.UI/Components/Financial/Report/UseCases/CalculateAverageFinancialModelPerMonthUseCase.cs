namespace SeahawkSaverFrontend.UI.Components.Financial.Report.UseCases;
using SeahawkSaverFrontend.Application.Abstractions.UseCases;
using SeahawkSaverFrontend.UI.Components.Financial.Report.DTOs;

/**
 * <summary>
 * A use case for calculating the average per month for a financial model.
 * </summary>
 */
public abstract class CalculateAverageFinancialModelPerMonthUseCase : UseCase<IEnumerable<FinancialModelMonthTotal>, decimal>
{
	/**
	 * <summary>
	 * Gets the month's total amount.
	 * </summary>
	 * <param name="financialModelMonthTotal">The financial month total.</param>
	 * <returns>The month's total amount.</returns>
	 */
	protected abstract decimal GetMonthTotalAmount(FinancialModelMonthTotal financialModelMonthTotal);

	public override Task<decimal> ExecuteAsync(IEnumerable<FinancialModelMonthTotal> input)
	{
		var financialModelMonthTotals = input.ToList();
		var total = 0.0m;

		if (financialModelMonthTotals.Count == 0)
		{
			return Task.FromResult(total);
		}

		var monthCount = 0;

		foreach (var financialModelMonthTotal in financialModelMonthTotals)
		{
			var monthTotal = GetMonthTotalAmount(financialModelMonthTotal);

			if (monthTotal == 0)
			{
				continue;
			}

			total += monthTotal;
			++monthCount;
		}

		return Task.FromResult(total / monthCount);
	}
}