namespace SeahawkSaverFrontend.UI.Components.Financial.Report.UseCases;
using SeahawkSaverFrontend.Application.Abstractions.UseCases;
using SeahawkSaverFrontend.UI.Components.Financial.Report.DTOs;

/**
 * <summary>
 * A use case to filter the current year monthly totals by cash flow.
 * </summary>
 */
public abstract class FilterCurrentYearMonthsByCashFlow : UseCase<IEnumerable<FinancialModelMonthTotal>, IEnumerable<FinancialModelMonthTotal>>
{
	/**
	 * <summary>
	 * Checks if the cash flow meets the requirements for the specific type.
	 * </summary>
	 */
	protected abstract bool DoesMonthMeetCashFlowType(decimal cashFlow);

	public override Task<IEnumerable<FinancialModelMonthTotal>> ExecuteAsync(IEnumerable<FinancialModelMonthTotal> input)
	{
		var filteredMonths = new List<FinancialModelMonthTotal>();

		foreach (var financialModelMonthTotal in input)
		{
			var totalIncome = financialModelMonthTotal.IncomeModelTotal.Amount;
			var totalExpenses = financialModelMonthTotal.DebtModelTotal.Amount +
								financialModelMonthTotal.ExpenseModelTotal.Amount +
								financialModelMonthTotal.SubscriptionModelTotal.Amount;

			var cashFlow = totalIncome - totalExpenses;

			var doesMonthMeetCashFlowType = DoesMonthMeetCashFlowType(cashFlow);

			if (!doesMonthMeetCashFlowType)
			{
				continue;
			}

			filteredMonths.Add(financialModelMonthTotal);
		}

		return Task.FromResult(filteredMonths.AsEnumerable());
	}
}