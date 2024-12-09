using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Components.Financial.Calendar.Legend;
public partial class FinancialModelCalendarItemBackgroundColorLegendEntryComponent : ComponentBase
{
	[Parameter]
	public string FinancialModelName { get; set; } = string.Empty;

	[Parameter]
	public string BackgroundColor { get; set; } = string.Empty;
}