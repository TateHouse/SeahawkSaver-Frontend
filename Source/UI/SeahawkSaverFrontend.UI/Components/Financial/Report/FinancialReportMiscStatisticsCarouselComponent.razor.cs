using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Components.Financial.Report;
public partial class FinancialReportMiscStatisticsCarouselComponent : ComponentBase, IDisposable
{
	[Parameter]
	public string CurrentYear { get; set; } = string.Empty;

	protected override void OnInitialized()
	{
		FinancialReportManager.OnStateChanged += StateHasChanged;
	}

	public void Dispose()
	{
		FinancialReportManager.OnStateChanged -= StateHasChanged;
	}

	private static string GetMonthText(int count, bool isUpperCase)
	{
		if (isUpperCase)
		{
			return count != 1 ? "Months" : "Month";

		}

		return count != 1 ? "months" : "month";
	}
}