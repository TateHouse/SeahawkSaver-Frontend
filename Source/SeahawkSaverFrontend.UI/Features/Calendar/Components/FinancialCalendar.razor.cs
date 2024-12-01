using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Features.Calendar.Components;
using Heron.MudCalendar;
using Heron.MudTotalCalendar;
using Microsoft.JSInterop;
using MudBlazor;
using SeahawkSaverFrontend.UI.Features.Calendar.DTOs;
using SeahawkSaverFrontend.UI.Features.Debt.DTOs;
using SeahawkSaverFrontend.UI.Features.Expense.DTOs;
using SeahawkSaverFrontend.UI.Features.Income.DTOs;
using SeahawkSaverFrontend.UI.Features.Saving.DTOs;
using SeahawkSaverFrontend.UI.Features.Subscription.DTOs;
using System.Globalization;
using System.Text;

public partial class FinancialCalendar : ComponentBase
{
	private List<DebtModel> debts = new List<DebtModel>();
	private List<IncomeModel> incomes = new List<IncomeModel>();
	private List<SavingModel> savings = new List<SavingModel>();
	private List<SubscriptionModel> subscriptions = new List<SubscriptionModel>();
	private List<ExpenseModel> expenses = new List<ExpenseModel>();

	private MudTotalCalendar calendar;
	private readonly List<CalendarItem> calendarItems = new List<CalendarItem>();
	private bool isWeekTotalEnabled = true;
	private bool isMonthTotalEnabled = true;

	private readonly double[] financialReportData = new double[5];
	private readonly string[] financialReportLabels = new string[5]
	{
		"Debt",
		"Income",
		"Saving",
		"Subscription",
		"Expense"
	};

	private readonly double[] currentMonthTotals = new double[5];
	private readonly string[] monthLabels = new string[12]
	{
		"Jan",
		"Feb",
		"Mar",
		"Apr",
		"May",
		"Jun",
		"Jul",
		"Aug",
		"Sep",
		"Oct",
		"Nov",
		"Dec"
	};

	protected override async Task OnInitializedAsync()
	{
		await LoadFinancialData();
		LoadCalendarEvents();
		UpdateFinancialReport();
		StateHasChanged();
	}

	private async Task LoadFinancialData()
	{
		debts = (await DebtService.GetDebtsAsync()).ToList();
		incomes = (await IncomeService.GetIncomesAsync()).ToList();
		savings = (await SavingService.GetSavingsAsync()).ToList();
		subscriptions = (await SubscriptionService.GetSubscriptionsAsync()).ToList();
		expenses = (await ExpenseService.GetExpensesAsync()).ToList();
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

		foreach (var expense in expenses)
		{
			var item = new FinancialItem<ExpenseModel>
			{
				FinancialItemType = FinancialItemType.Expense,
				Model = expense
			};

			item.Start = expense.DateTime!.Value;
			item.End = expense.DateTime!.Value.AddMinutes(1);
			item.AllDay = true;
			item.Text = $"Expense: ${expense.Amount}";

			calendarItems.Add(item);
		}
	}

	private List<Value> CalculateTotals()
	{
		var calendarTotals = new List<Value>();
		var totals = new Dictionary<string, ValueDefinition>
		{
			{
				"Debt",
				new ValueDefinition
				{
					Name = "Debt",
					Units = "$",
					PrefixUnits = true
				}
			},
			{
				"Income", new ValueDefinition
				{
					Name = "Income",
					Units = "$",
					PrefixUnits = true
				}
			},
			{
				"Saving", new ValueDefinition
				{
					Name = "Saving",
					Units = "$",
					PrefixUnits = true
				}
			},
			{
				"Subscription", new ValueDefinition
				{
					Name = "Subscription",
					Units = "$",
					PrefixUnits = true
				}
			},
			{
				"Expense", new ValueDefinition
				{
					Name = "Expense",
					Units = "$",
					PrefixUnits = true
				}
			}
		};

		foreach (var item in calendarItems)
		{
			var colonIndex = item.Text.IndexOf(':');
			var typeText = item.Text[..colonIndex];

			switch (typeText)
			{
				case "Debt":
					var debt = (FinancialItem<DebtModel>)item;
					var debtTotalEntry = new Value
					{
						Amount = (double)debt.Model.Amount,
						Date = debt.Model.DateTime!.Value,
						Definition = totals["Debt"]
					};

					calendarTotals.Add(debtTotalEntry);

					break;

				case "Income":
					var income = (FinancialItem<IncomeModel>)item;
					var incomeTotalEntry = new Value
					{
						Amount = (double)income.Model.Amount,
						Date = income.Model.DateTime!.Value,
						Definition = totals["Income"]
					};

					calendarTotals.Add(incomeTotalEntry);

					break;

				case "Saving":
					var saving = (FinancialItem<SavingModel>)item;
					var savingTotalEntry = new Value
					{
						Amount = (double)saving.Model.Amount,
						Date = saving.Model.DateTime!.Value,
						Definition = totals["Saving"]
					};

					calendarTotals.Add(savingTotalEntry);

					break;

				case "Subscription":
					var subscription = (FinancialItem<SubscriptionModel>)item;
					var subscriptionTotalEntry = new Value
					{
						Amount = (double)subscription.Model.Amount,
						Date = subscription.Model.DateTime!.Value,
						Definition = totals["Subscription"]
					};

					calendarTotals.Add(subscriptionTotalEntry);

					break;

				case "Expense":
					var expense = (FinancialItem<ExpenseModel>)item;
					var expenseTotalEntry = new Value
					{
						Amount = (double)expense.Model.Amount,
						Date = expense.Model.DateTime!.Value,
						Definition = totals["Expense"]
					};

					calendarTotals.Add(expenseTotalEntry);

					break;

				default:
					throw new ArgumentOutOfRangeException();
			}

		}

		return calendarTotals;
	}

	private void DateRangeChanged(DateRange dateRange)
	{
		ReloadCalendar();
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

				case FinancialItemType.Expense:
					var expense = new ExpenseModel
					{
						ExpenseId = model.Id,
						Amount = model.Amount,
						DateTime = model.DateTime
					};

					if (await ExpenseService.AddExpenseAsync(expense))
					{
						model.ErrorMessage = null;
						expenses.Add(expense);
					}

					break;

				default:
					throw new ArgumentOutOfRangeException();
			}

			ReloadCalendar();
			UpdateFinancialReport();
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

			case "Expense":
				var expenseItem = (FinancialItem<ExpenseModel>)item;
				entryModel = new FinancialEntryModel
				{
					Id = expenseItem.Model.ExpenseId,
					Amount = expenseItem.Model.Amount,
					DateTime = expenseItem.Model.DateTime,
					Type = FinancialItemType.Expense
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

			case FinancialItemType.Expense:
				var expense = expenses.Find(expense => expense.ExpenseId == model.Id);

				if (expense == null)
				{
					throw new InvalidOperationException();
				}
				expense.Amount = model.Amount;
				expense.DateTime = model.DateTime;
				model.ErrorMessage = await ExpenseService.UpdateExpenseAsync(expense) ? null : "An error occurred when updating the expense...";

				break;
		}

		ReloadCalendar();
		UpdateFinancialReport();
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

			case FinancialItemType.Expense:
				var expense = expenses.Find(expense => expense.ExpenseId == model.Id);

				if (expense == null)
				{
					throw new InvalidOperationException();
				}

				if (await ExpenseService.RemoveExpenseAsync(expense))
				{
					model.ErrorMessage = null;
					expenses.Remove(expense);
				}

				break;
		}

		ReloadCalendar();
		UpdateFinancialReport();
		StateHasChanged();

	}

	private void ReloadCalendar()
	{
		calendarItems.Clear();
		LoadCalendarEvents();
	}

	private async Task OnDownloadFinancialDataAsync()
	{
		var content = GenerateCSVContent();
		var fileName = $"SeahawkSaver_{DataCache.User.FirstName}{DataCache.User.LastName}_FinancialData_{DateTime.Now.ToString(CultureInfo.InvariantCulture)}.csv";
		await JSRuntime.InvokeVoidAsync("downloadCsvFile", content, fileName);
	}

	private sealed class CSVRow
	{
		public required decimal Amount { get; init; }
		public required DateTime? DateTime { get; init; }
		public required FinancialItemType Type { get; init; }
	}

	private string GenerateCSVContent()
	{
		var data = new List<CSVRow>();

		foreach (var debt in debts)
		{
			var row = new CSVRow
			{
				Amount = debt.Amount,
				DateTime = debt.DateTime,
				Type = FinancialItemType.Debt
			};

			data.Add(row);
		}

		foreach (var income in incomes)
		{
			var row = new CSVRow
			{
				Amount = income.Amount,
				DateTime = income.DateTime,
				Type = FinancialItemType.Income
			};

			data.Add(row);
		}

		foreach (var saving in savings)
		{
			var row = new CSVRow
			{
				Amount = saving.Amount,
				DateTime = saving.DateTime,
				Type = FinancialItemType.Saving
			};

			data.Add(row);
		}

		foreach (var subscription in subscriptions)
		{
			var row = new CSVRow
			{
				Amount = subscription.Amount,
				DateTime = subscription.DateTime,
				Type = FinancialItemType.Subscription
			};

			data.Add(row);
		}

		foreach (var expense in expenses)
		{
			var row = new CSVRow
			{
				Amount = expense.Amount,
				DateTime = expense.DateTime,
				Type = FinancialItemType.Expense
			};

			data.Add(row);
		}

		var sortedData = data.OrderBy(row => row.DateTime)
							 .ToList();

		var stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("Date,Amount,Type");

		foreach (var row in sortedData)
		{
			stringBuilder.AppendLine($"{row.DateTime!.Value.ToShortDateString()},{row.Amount},{row.Type}");
		}

		return stringBuilder.ToString();
	}

	private void UpdateFinancialReport()
	{
		financialReportData[0] = debts.Aggregate(0.0, (accumulator, debt) => accumulator + (double)debt.Amount);
		financialReportData[1] = incomes.Aggregate(0.0, (accumulator, income) => accumulator + (double)income.Amount);
		financialReportData[2] = savings.Aggregate(0.0, (accumulator, saving) => accumulator + (double)saving.Amount);
		financialReportData[3] = subscriptions.Aggregate(0.0, (accumulator, subscription) => accumulator + (double)subscription.Amount);
		financialReportData[4] = expenses.Aggregate(0.0, (accumulator, expense) => accumulator + (double)expense.Amount);
	}

	private sealed class MonthTotal
	{
		public required int Month { get; init; }
		public required double Amount { get; init; }
		public required FinancialItemType Type { get; init; }
	}

	private List<ChartSeries> UpdateLineChart()
	{
		var data = new List<ChartSeries>
		{
			new ChartSeries
			{
				Name = "Debt",
			},
			new ChartSeries
			{
				Name = "Income",
			},
			new ChartSeries
			{
				Name = "Saving",
			},
			new ChartSeries
			{
				Name = "Subscription",
			},
			new ChartSeries
			{
				Name = "Expense"
			}
		};

		var debtTotalsPerMonth = new double[12];
		var incomeTotalsPerMonth = new double[12];
		var savingTotalsPerMonth = new double[12];
		var subscriptionTotalPerMonth = new double[12];
		var expenseTotalPerMonth = new double[12];

		foreach (var debt in debts)
		{
			var debtMonthIndex = debt.DateTime!.Value.Month - 1;
			debtTotalsPerMonth[debtMonthIndex] += (double)debt.Amount;
		}

		foreach (var income in incomes)
		{
			var incomeMonthIndex = income.DateTime!.Value.Month - 1;
			incomeTotalsPerMonth[incomeMonthIndex] += (double)income.Amount;
		}

		foreach (var saving in savings)
		{
			var savingMonthIndex = saving.DateTime!.Value.Month - 1;
			savingTotalsPerMonth[savingMonthIndex] += (double)saving.Amount;
		}

		foreach (var subscription in subscriptions)
		{
			var subscriptionMonthIndex = subscription.DateTime!.Value.Month - 1;
			subscriptionTotalPerMonth[subscriptionMonthIndex] += (double)subscription.Amount;
		}

		foreach (var expense in expenses)
		{
			var expenseMonthIndex = expense.DateTime!.Value.Month - 1;
			expenseTotalPerMonth[expenseMonthIndex] += (double)expense.Amount;
		}

		data[0].Data = debtTotalsPerMonth;
		data[1].Data = incomeTotalsPerMonth;
		data[2].Data = savingTotalsPerMonth;
		data[3].Data = subscriptionTotalPerMonth;
		data[4].Data = expenseTotalPerMonth;

		var currentMonthIndex = calendar.CurrentDay.Month - 1;

		currentMonthTotals[0] = debtTotalsPerMonth[currentMonthIndex];
		currentMonthTotals[1] = incomeTotalsPerMonth[currentMonthIndex];
		currentMonthTotals[2] = savingTotalsPerMonth[currentMonthIndex];
		currentMonthTotals[3] = subscriptionTotalPerMonth[currentMonthIndex];
		currentMonthTotals[4] = expenseTotalPerMonth[currentMonthIndex];

		return data;
	}
}