using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Features.Calendar.Components;
using MudBlazor;
using SeahawkSaverFrontend.UI.Features.Calendar.DTOs;

public partial class FinancialCalendarEntryManageComponent : ComponentBase
{
	private const string AmountErrorMessage = "Invalid amount.";
	private const string DateErrorMessage = "Invalid date.";

	[CascadingParameter]
	public MudDialogInstance Dialog { get; set; }

	[Parameter]
	public FinancialEntryModel Model { get; set; }

	[Parameter]
	public EventCallback<FinancialEntryModel> OnSave { get; set; }

	[Parameter]
	public EventCallback<FinancialEntryModel> OnDelete { get; set; }

	private bool isAmountValid = false;
	private bool isDateValid = false;

	private void Save()
	{
		isAmountValid = true;
		isDateValid = true;

		if (Model.Amount <= 0)
		{
			isAmountValid = false;
			Model.ErrorMessage = "The amount must be greater than 0.";

			Snackbar.Add(Model.ErrorMessage);

			return;
		}

		if (Model.DateTime == null)
		{
			Model.ErrorMessage = "The date must be provided.";

			return;
		}

		if (Model.DateTime < DateTime.UtcNow.AddDays(-30) ||
			Model.DateTime > DateTime.UtcNow)
		{
			Model.ErrorMessage = "The date must be less than 30 days ago and cannot be in the future.";
			Snackbar.Add(Model.ErrorMessage);

			return;
		}

		OnSave.InvokeAsync(Model);
		Dialog.Close(Model);
	}

	private void Delete()
	{
		OnDelete.InvokeAsync(Model);
		Dialog.Close(Model);
	}

	private void Cancel()
	{
		Dialog.Close();
	}
}