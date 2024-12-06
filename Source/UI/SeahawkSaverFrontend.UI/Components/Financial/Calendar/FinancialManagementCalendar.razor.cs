using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Components.Financial.Calendar;
using Heron.MudCalendar;
using Heron.MudTotalCalendar;
using MudBlazor;
using SeahawkSaverFrontend.UI.Components.Financial.Calendar.Items;

public partial class FinancialManagementCalendar : ComponentBase
{
	private MudTotalCalendar calendar = null!;
	private Dictionary<char, bool> totalCalculationVisibilityToggles = new Dictionary<char, bool>
	{
		{ 'W', true },
		{ 'M', true }
	};

	protected override async Task OnInitializedAsync()
	{
		await FinancialModelCacheManager.LoadAsync();
		FinancialManagementCalendarItemManager.CreateFinancialModelCalendarItems();

		StateHasChanged();
	}

	private async Task OnCellClicked(DateTime dateTime)
	{

	}

	private async Task OnItemClicked(CalendarItem calendarItem)
	{
		var item = (FinancialModelCalendarItem)calendarItem;
	}

	private void OnDateRangeChanged(DateRange dateRange)
	{

	}

	private List<Value> CalculateTotals()
	{
		return new List<Value>();
	}
}