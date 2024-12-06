namespace SeahawkSaverFrontend.UI.Components.Financial.Calendar.Items;
using MudBlazor;

public static class FinancialModelCalendarItemColors
{
	public static readonly IReadOnlyDictionary<string, string> BackgroundColors = new Dictionary<string, string>
	{
		{ nameof(DebtModelCalendarItem), Colors.Red.Default },
		{ nameof(ExpenseModelCalendarItem), Colors.Orange.Default },
		{ nameof(IncomeModelCalendarItem), Colors.Green.Default },
		{ nameof(SavingModelCalendarItem), Colors.LightBlue.Default },
		{ nameof(SubscriptionModelCalendarItem), Colors.Yellow.Default },
	};
}