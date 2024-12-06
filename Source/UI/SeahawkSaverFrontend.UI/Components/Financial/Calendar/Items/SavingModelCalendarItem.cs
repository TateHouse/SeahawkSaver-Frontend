namespace SeahawkSaverFrontend.UI.Components.Financial.Calendar.Items;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A calendar item for <see cref="SavingModel"/>.
 * </summary>
 */
public sealed class SavingModelCalendarItem : FinancialModelCalendarItem
{
	public SavingModelCalendarItem()
	{
		HexColor = FinancialModelCalendarItemColors.BackgroundColors[nameof(SavingModelCalendarItem)];
	}
}