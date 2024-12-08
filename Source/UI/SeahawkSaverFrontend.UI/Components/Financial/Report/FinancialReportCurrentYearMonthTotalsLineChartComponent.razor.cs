using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Components.Financial.Report;
using MudBlazor;
using SeahawkSaverFrontend.Domain.Models.Financial;

public partial class FinancialReportCurrentYearMonthTotalsLineChartComponent : ComponentBase, IDisposable
{
	private readonly Dictionary<FinancialModelType, List<double>> financialModelMonthlyTotals = new Dictionary<FinancialModelType, List<double>>()
	{
		{
			FinancialModelType.Debt, Enumerable.Repeat(0.0, 12).ToList()
		},
		{
			FinancialModelType.Expense, Enumerable.Repeat(0.0, 12).ToList()
		},
		{
			FinancialModelType.Income, Enumerable.Repeat(0.0, 12).ToList()
		},
		{
			FinancialModelType.Saving, Enumerable.Repeat(0.0, 12).ToList()
		},
		{
			FinancialModelType.Subscription, Enumerable.Repeat(0.0, 12).ToList()
		}
	};

	private ChartOptions chartOptions = new ChartOptions
	{
		ShowLegend = true,
		XAxisLines = true,
		YAxisRequireZeroPoint = true,
		YAxisTicks = 100,
	};

	private List<ChartSeries> chartSeries = new List<ChartSeries>();
	private string[] xAxisLabels = new[]
	{
		"Jan",
		"Feb",
		"Mar",
		"Apr",
		"May",
		"Jun",
		"Jul",
		"Aug",
		"Sep",
		"Oct",
		"Nov",
		"Dec"
	};

	protected override void OnInitialized()
	{
		FinancialReportManager.OnStateChanged += StateHasChanged;

		foreach (var financialModelMonthTotal in FinancialReportManager.FinancialModelCurrentYearMonthTotals)
		{
			financialModelMonthlyTotals[FinancialModelType.Debt][financialModelMonthTotal.MonthIndex] = (double)financialModelMonthTotal.DebtModelTotal.Amount;
			financialModelMonthlyTotals[FinancialModelType.Expense][financialModelMonthTotal.MonthIndex] = (double)financialModelMonthTotal.ExpenseModelTotal.Amount;
			financialModelMonthlyTotals[FinancialModelType.Income][financialModelMonthTotal.MonthIndex] = (double)financialModelMonthTotal.IncomeModelTotal.Amount;
			financialModelMonthlyTotals[FinancialModelType.Saving][financialModelMonthTotal.MonthIndex] = (double)financialModelMonthTotal.SavingModelTotal.Amount;
			financialModelMonthlyTotals[FinancialModelType.Subscription][financialModelMonthTotal.MonthIndex] = (double)financialModelMonthTotal.SubscriptionModelTotal.Amount;
		}

		foreach (var pair in financialModelMonthlyTotals)
		{
			var series = CreateChartSeries(pair.Key);
			chartSeries.Add(series);
		}
	}

	public void Dispose()
	{
		FinancialReportManager.OnStateChanged -= StateHasChanged;
	}

	private ChartSeries CreateChartSeries(FinancialModelType financialModelType)
	{
		var name = financialModelType switch
				   {
					   FinancialModelType.Debt => "Debt",
					   FinancialModelType.Expense => "Expense",
					   FinancialModelType.Income => "Income",
					   FinancialModelType.Saving => "Saving",
					   FinancialModelType.Subscription => "Subscription",
					   var _ => string.Empty
				   };

		return new ChartSeries
		{
			Data = financialModelMonthlyTotals[financialModelType].ToArray(),
			Name = name,
			Visible = true
		};
	}
}