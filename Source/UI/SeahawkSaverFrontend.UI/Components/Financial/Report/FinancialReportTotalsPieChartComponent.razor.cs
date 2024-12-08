using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Components.Financial.Report;
using SeahawkSaverFrontend.Domain.Models.Financial;

public partial class FinancialReportTotalsPieChartComponent : ComponentBase
{
	private readonly List<double> chartData = new List<double>
	{
		0.0, 0.0, 0.0, 0.0, 0.0
	};

	private readonly string[] chartLabels = new[]
	{
		"Debt",
		"Expense",
		"Income",
		"Saving",
		"Subscription"
	};

	protected override void OnInitialized()
	{
		foreach (var financialModelTotal in FinancialReportManager.FinancialModelOverallTotals)
		{
			var amount = (double)financialModelTotal.Amount;

			switch (financialModelTotal.Type)
			{
				case FinancialModelType.Debt:
					chartData[0] = amount;

					break;

				case FinancialModelType.Expense:
					chartData[1] = amount;

					break;

				case FinancialModelType.Income:
					chartData[2] = amount;

					break;

				case FinancialModelType.Saving:
					chartData[3] = amount;

					break;

				case FinancialModelType.Subscription:
					chartData[4] = amount;

					break;
			}
		}
	}
}