using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Components.Financial.Report;
public partial class FinancialReportPage : ComponentBase, IDisposable
{
	protected override void OnInitialized()
	{
		FinancialReportManager.OnStateChanged += StateHasChanged;
	}

	public void Dispose()
	{
		FinancialReportManager.OnStateChanged -= StateHasChanged;
	}

	private static string GetCurrentYear()
	{
		var dateTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);

		return dateTime.ToString("yyyy");
	}
}