using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Components.Financial.Report;
public partial class FinancialReportPage : ComponentBase
{
	private static string GetCurrentYear()
	{
		var dateTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);

		return dateTime.ToString("yyyy");
	}
}