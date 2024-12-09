namespace SeahawkSaverFrontend.UI.Themes;
using MudBlazor;
using MudBlazor.Utilities;

/**
 * <summary>
 * A custom <see cref="MudTheme"/> for the application to be styled with the University of North Carolina Wilmington's
 * colors.
 * </summary>
 */
public sealed class SeahawkTheme : MudTheme
{
	private static readonly MudColor Teal = new MudColor(0, 112, 115, 255);
	private static readonly MudColor Navy = new MudColor(0, 51, 102, 255);
	private static readonly MudColor Gold = new MudColor(249, 227, 127, 255);

	/**
	 * <summary>
	 * Instantiates a new <see cref="SeahawkTheme"/> instance.
	 * </summary>
	 */
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