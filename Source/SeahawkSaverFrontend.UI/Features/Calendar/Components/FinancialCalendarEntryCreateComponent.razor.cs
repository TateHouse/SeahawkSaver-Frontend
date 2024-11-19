using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Features.Calendar.Components;
using MudBlazor;
using SeahawkSaverFrontend.UI.Features.Calendar.DTOs;

public partial class FinancialCalendarEntryCreateComponent : ComponentBase
{
	private const string AmountErrorMessage = "The amount is required";

	[CascadingParameter]
	public MudDialogInstance Dialog { get; set; }

	[Parameter]
	public DateTime DateTime { get; set; }

	private readonly FinancialEntryModel model = new FinancialEntryModel();
	private bool isAmountValid = false;
	private bool isDateValid = false;

	private void Submit()
	{
		isAmountValid = true;
		model.DateTime = DateTime;

		if (model.Amount <= 0)
		{
			isAmountValid = false;
			model.ErrorMessage = "The amount must be greater than 0.";

			Snackbar.Add(model.ErrorMessage);

			return;
		}

		if (model.DateTime == null)
		{
			model.ErrorMessage = "The date must be provided.";

			return;
		}

		if (model.DateTime < DateTime.UtcNow.AddDays(-30) ||
			model.DateTime > DateTime.UtcNow)
		{
			model.ErrorMessage = "The date must be less than 30 days ago and cannot be in the future.";
			Snackbar.Add(model.ErrorMessage);

			return;
		}

		Dialog.Close(model);
	}

	private void Cancel()
	{
		Dialog.Close();
	}
}