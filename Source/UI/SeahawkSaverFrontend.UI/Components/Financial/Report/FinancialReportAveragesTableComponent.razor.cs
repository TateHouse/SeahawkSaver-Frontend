using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Components.Financial.Report;
using SeahawkSaverFrontend.Domain.Models.Financial;

public partial class FinancialReportAveragesTableComponent : ComponentBase, IDisposable
{
	private sealed record TableRow
	{
		public required decimal MonthlyAverage { get; init; }
		public required FinancialModelType Type { get; init; }
	}

	private IEnumerable<TableRow> financialModelMonthlyAverages = null!;

	protected override void OnInitialized()
	{
		FinancialReportManager.OnStateChanged += StateHasChanged;
		financialModelMonthlyAverages = new List<TableRow>
		{
			new TableRow
			{
				MonthlyAverage = FinancialReportManager.AverageFinancialModelsPerMonth.AverageDebtAmount,
				Type = FinancialModelType.Debt
			},
			new TableRow
			{
				MonthlyAverage = FinancialReportManager.AverageFinancialModelsPerMonth.AverageExpenseAmount,
				Type = FinancialModelType.Expense
			},
			new TableRow
			{
				MonthlyAverage = FinancialReportManager.AverageFinancialModelsPerMonth.AverageIncomeAmount,
				Type = FinancialModelType.Income
			},
			new TableRow
			{
				MonthlyAverage = FinancialReportManager.AverageFinancialModelsPerMonth.AverageSavingAmount,
				Type = FinancialModelType.Saving
			},
			new TableRow
			{
				MonthlyAverage = FinancialReportManager.AverageFinancialModelsPerMonth.AverageSubscriptionAmount,
				Type = FinancialModelType.Subscription
			}
		};
	}

	public void Dispose()
	{
		FinancialReportManager.OnStateChanged -= StateHasChanged;
	}
}