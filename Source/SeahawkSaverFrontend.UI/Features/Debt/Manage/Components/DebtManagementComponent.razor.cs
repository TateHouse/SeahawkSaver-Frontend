using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Features.Debt.Manage.Components;
using SeahawkSaverFrontend.UI.Features.Debt.Manage.DTOs;

public partial class DebtManagementComponent : ComponentBase
{
	private List<DebtEntryModel> debts = new List<DebtEntryModel>();

	protected override async Task OnInitializedAsync()
	{
		debts = (await DebtService.GetDebtsAsync())
				.Select(debt => new DebtEntryModel
				{
					DebtId = debt.DebtId,
					Amount = debt.Amount,
					DateTime = debt.DateTime,
					IsEditable = false
				})
				.ToList();

		StateHasChanged();
	}

	private async Task Add()
	{
		var dialog = await DialogService.ShowAsync<CreateDebtFormComponent>();
		var dialogResult = await dialog.Result;

		if (!dialogResult.Canceled && dialogResult.Data is DebtEntryModel model)
		{
			var result = await DebtService.AddDebtAsync(model);

			if (result)
			{
				model.ErrorMessage = null;
				debts.Add(model);
			}
			else
			{
				model.ErrorMessage = "An error occurred when adding the debt...";
			}

			StateHasChanged();
		}
	}

	private async Task Save(DebtEntryModel model)
	{
		var result = await DebtService.UpdateDebtAsync(model);
		model.ErrorMessage = result ? null : "An error occurred when updating the debt...";
		StateHasChanged();
	}

	private async Task Delete(DebtEntryModel model)
	{
		var result = await DebtService.RemoveDebtAsync(model);

		if (result)
		{
			model.ErrorMessage = null;
			debts.Remove(model);
		}
		else
		{
			model.ErrorMessage = "An error occurred when deleting the debt...";
		}

		StateHasChanged();
	}
}