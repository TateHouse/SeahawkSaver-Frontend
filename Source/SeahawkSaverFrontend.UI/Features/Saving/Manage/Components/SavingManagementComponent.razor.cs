using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Features.Saving.Manage.Components;
using SeahawkSaverFrontend.UI.Features.Saving.Manage.DTOs;

public partial class SavingManagementComponent : ComponentBase
{
	private List<SavingEntryModel> savings = new List<SavingEntryModel>();

	protected override async Task OnInitializedAsync()
	{
		savings = (await SavingService.GetSavingsAsync())
				  .Select(saving => new SavingEntryModel
				  {
					  SavingId = saving.SavingId,
					  Amount = saving.Amount,
					  DateTime = saving.DateTime,
					  IsEditable = false
				  })
				  .ToList();

		StateHasChanged();
	}

	private async Task Add()
	{

		var dialog = await DialogService.ShowAsync<CreateSavingFormComponent>();
		var dialogResult = await dialog.Result;

		if (!dialogResult.Canceled && dialogResult.Data is SavingEntryModel model)
		{
			var result = await SavingService.AddSavingAsync(model);

			if (result)
			{
				model.ErrorMessage = null;
				savings.Add(model);
			}
			else
			{
				model.ErrorMessage = "An error occurred when adding the saving...";
			}

			StateHasChanged();
		}
	}

	private async Task Save(SavingEntryModel model)
	{
		var result = await SavingService.UpdateSavingAsync(model);
		model.ErrorMessage = result ? null : "An error occurred when updating the saving...";
		StateHasChanged();
	}

	private async Task Delete(SavingEntryModel model)
	{
		var result = await SavingService.RemoveSavingAsync(model);

		if (result)
		{
			model.ErrorMessage = null;
			savings.Remove(model);
		}
		else
		{
			model.ErrorMessage = "An error occurred when deleting the saving...";
		}

		StateHasChanged();
	}
}