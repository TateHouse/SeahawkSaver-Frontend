using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Components.Financial.Report;
using MudBlazor;
using SeahawkSaverFrontend.Domain.Models.Financial;
using SeahawkSaverFrontend.UI.Components.Financial.Report.DTOs;

public partial class FinancialReportCurrentYearMonthTotalsLineChartComponent : ComponentBase
{
	private readonly Dictionary<FinancialModelType, List<double>> chartData = new Dictionary<FinancialModelType, List<double>>()
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

	[Parameter]
	public IEnumerable<FinancialModelMonthTotal> FinancialModelMonthTotals { get; set; }

	protected override void OnParametersSet()
	{
		foreach (var financialModelMonthTotal in FinancialModelMonthTotals)
		{
			chartData[FinancialModelType.Debt][financialModelMonthTotal.MonthIndex] = (double)financialModelMonthTotal.DebtModelTotal.Amount;
			chartData[FinancialModelType.Expense][financialModelMonthTotal.MonthIndex] = (double)financialModelMonthTotal.ExpenseModelTotal.Amount;
			chartData[FinancialModelType.Income][financialModelMonthTotal.MonthIndex] = (double)financialModelMonthTotal.IncomeModelTotal.Amount;
			chartData[FinancialModelType.Saving][financialModelMonthTotal.MonthIndex] = (double)financialModelMonthTotal.SavingModelTotal.Amount;
			chartData[FinancialModelType.Subscription][financialModelMonthTotal.MonthIndex] = (double)financialModelMonthTotal.SubscriptionModelTotal.Amount;
		}

		foreach (var pair in chartData)
		{
			var series = CreateChartSeries(pair.Key);
			chartSeries.Add(series);
		}
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
			Data = chartData[financialModelType].ToArray(),
			Name = name,
			Visible = true
		};
	}
}