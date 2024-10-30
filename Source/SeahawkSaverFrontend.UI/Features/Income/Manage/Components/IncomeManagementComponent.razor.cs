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

	private async Task Save(IncomeEntryModel model)
	{

	}

	private async Task Delete(IncomeEntryModel model)
	{
		incomes.Remove(model);
		StateHasChanged();
	}
}