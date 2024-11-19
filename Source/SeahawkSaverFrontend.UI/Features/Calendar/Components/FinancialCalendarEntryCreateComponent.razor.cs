using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Features.Calendar.Components;
using MudBlazor;
using SeahawkSaverFrontend.UI.Features.Calendar.DTOs;

public partial class FinancialCalendarEntryCreateComponent : ComponentBase
{
	private const string AmountErrorMessage = "The amount is required";
	private const string DateErrorMessage = "The date is required.";

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
		isDateValid = true;
		model.DateTime = DateTime;

		if (model.Amount <= 0)
		{
			isAmountValid = false;

			return;
		}

		if (model.DateTime == null ||
			model.DateTime < DateTime.UtcNow.AddDays(-30) ||
			model.DateTime > DateTime.UtcNow)
		{
			isDateValid = false;

			return;
		}

		Dialog.Close(model);
	}

	private void Cancel()
	{
		Dialog.Close();
	}
}