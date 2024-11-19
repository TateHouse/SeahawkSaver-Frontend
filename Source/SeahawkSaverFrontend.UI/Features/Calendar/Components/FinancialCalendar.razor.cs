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

	private async Task ItemClicked(CalendarItem item)
	{
		var colonIndex = item.Text.IndexOf(':');
		var typeText = item.Text[..colonIndex];
		FinancialEntryModel? entryModel = null;

		switch (typeText)
		{
			case "Debt":
				var debtItem = (FinancialItem<DebtModel>)item;
				entryModel = new FinancialEntryModel
				{
					Id = debtItem.Model.DebtId,
					Amount = debtItem.Model.Amount,
					DateTime = debtItem.Model.DateTime,
					Type = FinancialItemType.Debt
				};

				break;

			case "Income":
				var incomeItem = (FinancialItem<IncomeModel>)item;
				entryModel = new FinancialEntryModel
				{
					Id = incomeItem.Model.IncomeId,
					Amount = incomeItem.Model.Amount,
					DateTime = incomeItem.Model.DateTime,
					Type = FinancialItemType.Income
				};

				break;

			case "Saving":
				var savingItem = (FinancialItem<SavingModel>)item;
				entryModel = new FinancialEntryModel
				{
					Id = savingItem.Model.SavingId,
					Amount = savingItem.Model.Amount,
					DateTime = savingItem.Model.DateTime,
					Type = FinancialItemType.Saving
				};

				break;

			case "Subscription":
				var subscriptionItem = (FinancialItem<SubscriptionModel>)item;
				entryModel = new FinancialEntryModel
				{
					Id = subscriptionItem.Model.SubscriptionId,
					Amount = subscriptionItem.Model.Amount,
					DateTime = subscriptionItem.Model.DateTime,
					Type = FinancialItemType.Subscription
				};

				break;

			default:
				throw new ArgumentOutOfRangeException();
		}

		var parameters = new DialogParameters
		{
			{ "Model", entryModel },
			{ "OnSave", EventCallback.Factory.Create(this, (FinancialEntryModel model) => OnSave(model)) },
			{ "OnDelete", EventCallback.Factory.Create(this, (FinancialEntryModel model) => OnDelete(model)) }
		};

		await DialogService.ShowAsync<FinancialCalendarEntryManageComponent>("Manage", parameters);
	}

	private async Task OnSave(FinancialEntryModel model)
	{
		switch (model.Type)
		{
			case FinancialItemType.Debt:
				var debt = debts.Find(debt => debt.DebtId == model.Id);

				if (debt == null)
				{
					throw new InvalidOperationException();
				}

				debt.Amount = model.Amount;
				debt.DateTime = model.DateTime;
				model.ErrorMessage = await DebtService.UpdateDebtAsync(debt) ? null : "An error occurred when updating the debt...";

				break;

			case FinancialItemType.Income:
				var income = incomes.Find(income => income.IncomeId == model.Id);

				if (income == null)
				{
					throw new InvalidOperationException();
				}

				income.Amount = model.Amount;
				income.DateTime = model.DateTime;
				model.ErrorMessage = await IncomeService.UpdateIncomeAsync(income) ? null : "An error occurred when updating the income...";

				break;

			case FinancialItemType.Saving:
				var saving = savings.Find(saving => saving.SavingId == model.Id);

				if (saving == null)
				{
					throw new InvalidOperationException();
				}

				saving.Amount = model.Amount;
				saving.DateTime = model.DateTime;
				model.ErrorMessage = await SavingService.UpdateSavingAsync(saving) ? null : "An error occurred when updating the saving...";

				break;

			case FinancialItemType.Subscription:
				var subscription = subscriptions.Find(subscription => subscription.SubscriptionId == model.Id);

				if (subscription == null)
				{
					throw new InvalidOperationException();
				}

				subscription.Amount = model.Amount;
				subscription.DateTime = model.DateTime;
				model.ErrorMessage = await SubscriptionService.UpdateSubscriptionAsync(subscription) ? null : "An error occurred when updating the subscription...";

				break;
		}

		calendarItems.Clear();
		LoadCalendarEvents();
		StateHasChanged();
	}

	private async Task OnDelete(FinancialEntryModel model)
	{
		switch (model.Type)
		{
			case FinancialItemType.Debt:
				var debt = debts.Find(debt => debt.DebtId == model.Id);

				if (debt == null)
				{
					throw new InvalidOperationException();
				}

				if (await DebtService.RemoveDebtAsync(debt))
				{
					model.ErrorMessage = null;
					debts.Remove(debt);
				}
				else
				{
					model.ErrorMessage = "An error occurred when deleting the debt...";
				}

				break;

			case FinancialItemType.Income:
				var income = incomes.Find(income => income.IncomeId == model.Id);

				if (income == null)
				{
					throw new InvalidOperationException();
				}

				if (await IncomeService.RemoveIncomeAsync(income))
				{
					model.ErrorMessage = null;
					incomes.Remove(income);
				}
				else
				{
					model.ErrorMessage = "An error occurred when deleting the income...";
				}

				break;

			case FinancialItemType.Saving:
				var saving = savings.Find(saving => saving.SavingId == model.Id);

				if (saving == null)
				{
					throw new InvalidOperationException();
				}

				if (await SavingService.RemoveSavingAsync(saving))
				{
					model.ErrorMessage = null;
					savings.Remove(saving);
				}
				else
				{
					model.ErrorMessage = "An error occurred when deleting the saving...";
				}

				break;

			case FinancialItemType.Subscription:
				var subscription = subscriptions.Find(subscription => subscription.SubscriptionId == model.Id);

				if (subscription == null)
				{
					throw new InvalidOperationException();
				}

				if (await SubscriptionService.RemoveSubscriptionAsync(subscription))
				{
					model.ErrorMessage = null;
					subscriptions.Remove(subscription);
				}
				else
				{
					model.ErrorMessage = "An error occurred when deleting the subscription...";
				}

				break;
		}

		calendarItems.Clear();
		LoadCalendarEvents();
		StateHasChanged();
	}
}