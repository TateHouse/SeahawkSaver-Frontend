using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Components.Financial.Report;
using MudBlazor;

public partial class FinancialReportMiscStatisticsCarouselItemComponent : ComponentBase
{
	[Parameter]
	public Color Color { get; set; }

	[Parameter]
	public RenderFragment? ChildContent { get; set; }
}