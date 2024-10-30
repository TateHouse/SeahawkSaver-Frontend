namespace SeahawkSaverFrontend.UI.Utilities;
using MudBlazor;
using MudBlazor.Utilities;

public class SeahawkTheme : MudTheme
{
	private readonly static MudColor Teal = new MudColor(0, 112, 115, 255);
	private readonly static MudColor Navy = new MudColor(0, 51, 102, 255);
	private readonly static MudColor Gold = new MudColor(249, 227, 127, 255);

	public SeahawkTheme()
	{
		PaletteLight = new PaletteLight
		{
			AppbarBackground = SeahawkTheme.Teal,
			Primary = SeahawkTheme.Teal,
			Secondary = SeahawkTheme.Navy,
			Tertiary = SeahawkTheme.Gold
		};
	}
}