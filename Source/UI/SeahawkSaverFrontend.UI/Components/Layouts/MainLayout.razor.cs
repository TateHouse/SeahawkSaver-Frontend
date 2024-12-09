using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Components.Layouts;
using MudBlazor;
using SeahawkSaverFrontend.UI.Themes;

public partial class MainLayout : LayoutComponentBase
{
	private readonly MudTheme mudTheme = new SeahawkTheme();
}