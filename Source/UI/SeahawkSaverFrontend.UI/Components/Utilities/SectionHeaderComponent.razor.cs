using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Components.Utilities;
using MudBlazor;

public partial class SectionHeaderComponent : ComponentBase
{
	[Parameter]
	public Align Align { get; set; }

	[Parameter]
	public string Class { get; set; } = string.Empty;

	[Parameter]
	public string Style { get; set; } = string.Empty;

	[Parameter]
	public Typo Typo { get; set; }

	[Parameter]
	public RenderFragment? ChildContent { get; set; }
}