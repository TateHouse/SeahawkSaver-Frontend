using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Features.Income.Manage.Components;
using MudBlazor;
using SeahawkSaverFrontend.UI.Features.Income.Manage.DTOs;

public partial class CreateIncomeFormComponent : ComponentBase
{
	[CascadingParameter]
	public MudDialogInstance Dialog { get; set; }

	private IncomeEntryModel model = new IncomeEntryModel();

	private bool isAmountValid = false;
	private bool isDateValid = false;
	private string amountErrorMessage = "The amount is required";
	private string dateErrorMessage = "The date is required.";

	private void Submit()
	{
		isAmountValid = true;
		isDateValid = true;

		if (model.Amount <= 0)
		{
			isAmountValid = false;
		}

		if (model.DateTime == null ||
			model.DateTime < DateTime.UtcNow.AddDays(-30) ||
			model.DateTime > DateTime.UtcNow)
		{
			isDateValid = false;
		}

		if (!isAmountValid || !isDateValid)
		{
			return;
		}

		Dialog.Close(model);
	}

	private void Cancel()
	{
		Dialog.Close();
	}
}