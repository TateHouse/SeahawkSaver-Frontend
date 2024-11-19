using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Features.Calendar.Components;
using Heron.MudCalendar;
using MudBlazor;
using SeahawkSaverFrontend.UI.Features.Calendar.DTOs;
using SeahawkSaverFrontend.UI.Features.Debt.DTOs;
using SeahawkSaverFrontend.UI.Features.Income.DTOs;
using SeahawkSaverFrontend.UI.Features.Saving.DTOs;
using SeahawkSaverFrontend.UI.Features.Subscription.DTOs;

public partial class FinancialCalendar : ComponentBase
{
	private List<DebtModel> debts = new List<DebtModel>();
	private List<IncomeModel> incomes = new List<IncomeModel>();
	private List<SavingModel> savings = new List<SavingModel>();
	private List<SubscriptionModel> subscriptions = new List<SubscriptionModel>();

	private List<CalendarItem> calendarItems = new List<CalendarItem>();

	protected override async Task OnInitializedAsync()
	{
		await LoadFinancialData();
		LoadCalendarEvents();

		StateHasChanged();
	}

	private async Task LoadFinancialData()
	{
		debts = (await DebtService.GetDebtsAsync()).ToList();
		incomes = (await IncomeService.GetIncomesAsync()).ToList();
		savings = (await SavingService.GetSavingsAsync()).ToList();
		subscriptions = (await SubscriptionService.GetSubscriptionsAsync()).ToList();
	}

	private void LoadCalendarEvents()
	{
		foreach (var debt in debts)
		{
			var item = new FinancialItem<DebtModel>
			{
				FinancialItemType = FinancialItemType.Debt,
				Model = debt
			};

			item.Start = debt.DateTime!.Value;
			item.End = debt.DateTime!.Value.AddMinutes(1);
			item.AllDay = true;
			item.Text = $"Debt: ${debt.Amount}";

			calendarItems.Add(item);
		}

		foreach (var income in incomes)
		{
			var item = new FinancialItem<IncomeModel>
			{
				FinancialItemType = FinancialItemType.Income,
				Model = income
			};

			item.Start = income.DateTime!.Value;
			item.End = income.DateTime!.Value.AddMinutes(1);
			item.AllDay = true;
			item.Text = $"Income: ${income.Amount}";

			calendarItems.Add(item);
		}

		foreach (var saving in savings)
		{
			var item = new FinancialItem<SavingModel>
			{
				FinancialItemType = FinancialItemType.Saving,
				Model = saving
			};

			item.Start = saving.DateTime!.Value;
			item.End = saving.DateTime!.Value.AddMinutes(1);
			item.AllDay = true;
			item.Text = $"Saving: ${saving.Amount}";

			calendarItems.Add(item);
		}

		foreach (var subscription in subscriptions)
		{
			var item = new FinancialItem<SubscriptionModel>
			{
				FinancialItemType = FinancialItemType.Subscription,
				Model = subscription
			};

			item.Start = subscription.DateTime!.Value;
			item.End = subscription.DateTime!.Value.AddMinutes(1);
			item.AllDay = true;
			item.Text = $"Subscription: ${subscription.Amount}";

			calendarItems.Add(item);
		}
	}

	private void DateRangeChanged(DateRange dateRange)
	{
		calendarItems.Clear();
		LoadCalendarEvents();
	}

	private async Task CellClicked(DateTime dateTime)
	{
		var parameters = new DialogParameters
		{
			{ "DateTime", dateTime }
		};

		var dialog = await DialogService.ShowAsync<FinancialCalendarEntryCreateComponent>("Create", parameters);
		var dialogResult = await dialog.Result;

		if (!dialogResult.Canceled && dialogResult.Data is FinancialEntryModel model)
		{
			switch (model.Type)
			{
				case FinancialItemType.Debt:
					var debt = new DebtModel
					{
						DebtId = model.Id,
						Amount = model.Amount,
						DateTime = model.DateTime
					};

					if (await DebtService.AddDebtAsync(debt))
					{
						model.ErrorMessage = null;
						debts.Add(debt);
					}

					break;

				case FinancialItemType.Income:
					var income = new IncomeModel
					{
						IncomeId = model.Id,
						Amount = model.Amount,
						DateTime = model.DateTime
					};

					if (await IncomeService.AddIncomeAsync(income))
					{
						model.ErrorMessage = null;
						incomes.Add(income);
					}

					break;

				case FinancialItemType.Saving:
					var saving = new SavingModel
					{
						SavingId = model.Id,
						Amount = model.Amount,
						DateTime = model.DateTime
					};

					if (await SavingService.AddSavingAsync(saving))
					{
						model.ErrorMessage = null;
						savings.Add(saving);
					}

					break;

				case FinancialItemType.Subscription:
					var subscription = new SubscriptionModel
					{
						SubscriptionId = model.Id,
						Amount = model.Amount,
						DateTime = model.DateTime
					};

					if (await SubscriptionService.AddSubscriptionAsync(subscription))
					{
						model.ErrorMessage = null;
						subscriptions.Add(subscription);
					}

					break;

				default:
					throw new ArgumentOutOfRangeException();
			}

			calendarItems.Clear();
			LoadCalendarEvents();
			StateHasChanged();
		}
	}
}