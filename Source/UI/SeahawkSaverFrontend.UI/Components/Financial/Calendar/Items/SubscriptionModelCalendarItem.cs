namespace SeahawkSaverFrontend.UI.Components.Financial.Calendar.Items;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A calendar item for <see cref="SubscriptionModel"/>.
 * </summary>
 */
public sealed class SubscriptionModelCalendarItem : FinancialModelCalendarItem
{
	public SubscriptionModelCalendarItem()
	{
		HexColor = FinancialModelCalendarItemColors.BackgroundColors[nameof(SubscriptionModelCalendarItem)];
	}
}