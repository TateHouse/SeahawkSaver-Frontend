using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Components.Financial.Calendar;
using Heron.MudCalendar;
using Heron.MudTotalCalendar;
using MudBlazor;
using SeahawkSaverFrontend.UI.Components.Financial.Calendar.Items;
using SeahawkSaverFrontend.UI.Components.Financial.Calendar.Items.Create;
using SeahawkSaverFrontend.UI.Components.Financial.Calendar.Items.Download;
using SeahawkSaverFrontend.UI.Components.Financial.Calendar.Items.Manage;

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
		var financialModelCalendarItem = (FinancialModelCalendarItem)calendarItem;
		var parameters = new DialogParameters
		{
			{ "FinancialModelType", financialModelCalendarItem.FinancialModelType },
			{ "FinancialModel", financialModelCalendarItem.FinancialModel },
		};

		var dialog = await DialogService.ShowAsync<FinancialManagementCalendarManageFinancialModelComponent>("Manage", parameters);
		var dialogResult = await dialog.Result;

		if (!dialogResult.Canceled && dialogResult.Data is true)
		{
			FinancialManagementCalendarItemManager.ReloadFinancialModelCalendarItems();
			StateHasChanged();
		}
	}

	private async Task OnClick_DownloadData()
	{
		await DialogService.ShowAsync<FinancialModelCSVDownloadComponent>("Download Data");
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