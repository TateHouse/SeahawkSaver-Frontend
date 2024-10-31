namespace SeahawkSaverFrontend.UI.Features.Income.Manage.Components;
using Microsoft.AspNetCore.Components;
using SeahawkSaverFrontend.UI.Features.Income.Manage.DTOs;

public partial class IncomeManagementComponent : ComponentBase
{
	private List<IncomeEntryModel> incomes = new List<IncomeEntryModel>();

	protected override async Task OnInitializedAsync()
	{
		incomes = (await IncomeService.GetIncomes())
				  .Select(income => new IncomeEntryModel
				  {
					  IncomeId = income.IncomeId,
					  Amount = income.Amount,
					  DateTime = income.DateTime,
					  IsEditable = false
				  })
				  .ToList();

		StateHasChanged();
	}

	private async Task Add()
	{
		var dialog = await DialogService.ShowAsync<CreateIncomeFormComponent>();
		var dialogResult = await dialog.Result;

		if (!dialogResult.Canceled && dialogResult.Data is IncomeEntryModel model)
		{
			var result = await IncomeService.AddIncome(model);

			if (result)
			{
				model.ErrorMessage = null;
				incomes.Add(model);
			}
			else
			{
				model.ErrorMessage = "An error occurred when adding the income...";
			}

			StateHasChanged();
		}
	}

	private async Task Save(IncomeEntryModel model)
	{
		var result = await IncomeService.UpdateIncome(model);
		model.ErrorMessage = result ? null : "An error occurred when updating the income...";
		StateHasChanged();
	}

	private async Task Delete(IncomeEntryModel model)
	{
		var result = await IncomeService.RemoveIncome(model);

		if (result)
		{
			model.ErrorMessage = null;
			incomes.Remove(model);
		}
		else
		{
			model.ErrorMessage = "An error occurred when deleting the income...";
		}

		StateHasChanged();
	}
}