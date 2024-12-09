namespace SeahawkSaverFrontend.UI.Components.Financial.Calendar.Items;
using Heron.MudCalendar;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * An abstract base class for calendar items.
 * </summary>
 */
public abstract class FinancialModelCalendarItem : CalendarItem
{
	public required FinancialModel FinancialModel { get; init; }
	public required FinancialModelType FinancialModelType { get; init; }
	public string HexColor { get; init; } = string.Empty;
}