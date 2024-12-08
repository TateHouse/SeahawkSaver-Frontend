using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Components.Financial.Report;
public partial class FinancialReportMiscStatisticsCarouselComponent : ComponentBase
{
	private static string GetMonthText(int count, bool isUpperCase)
	{
		if (isUpperCase)
		{
			return count != 1 ? "Months" : "Month";

		}

		return count != 1 ? "months" : "month";
	}
}