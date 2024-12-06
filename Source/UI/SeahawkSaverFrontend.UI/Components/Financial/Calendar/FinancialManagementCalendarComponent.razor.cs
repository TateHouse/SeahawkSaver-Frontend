using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Components.Financial.Calendar;
using Heron.MudCalendar;
using Heron.MudTotalCalendar;
using MudBlazor;
using SeahawkSaverFrontend.UI.Components.Financial.Calendar.Items;
using SeahawkSaverFrontend.UI.Components.Financial.Calendar.Items.Create;

public partial class FinancialManagementCalendarComponent : ComponentBase
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
		FinancialManagementCalendarItemManager.ReloadFinancialModelCalendarItems();

		StateHasChanged();
	}

	private async Task OnCellClicked(DateTime dateTime)
	{
		var parameters = new DialogParameters
		{
			{ "DateTime", dateTime }
		};

		var dialog = await DialogService.ShowAsync<FinancialManagementCalendarCreateFinancialModelComponent>("Create", parameters);
		var dialogResult = await dialog.Result;

		if (!dialogResult.Canceled && dialogResult.Data is true)
		{
			FinancialManagementCalendarItemManager.ReloadFinancialModelCalendarItems();
			StateHasChanged();
		}
	}

	private async Task OnItemClicked(CalendarItem calendarItem)
	{
		var item = (FinancialModelCalendarItem)calendarItem;
	}

	private void OnDateRangeChanged(DateRange dateRange)
	{
		FinancialManagementCalendarItemManager.ReloadFinancialModelCalendarItems();
	}

	private List<Value> CalculateTotals()
	{
		return new List<Value>();
	}
}