namespace SeahawkSaverFrontend.UI.Features.Calendar;
using Heron.MudCalendar;

public class FinancialItem<TModel> : CalendarItem
{
	public required FinancialItemType FinancialItemType { get; init; }
	public required TModel Model { get; init; }
}