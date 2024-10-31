namespace SeahawkSaverFrontend.UI.Features.Income.Manage.Components;
using Microsoft.AspNetCore.Components;
using SeahawkSaverFrontend.UI.Features.Income.Manage.DTOs;

public partial class IncomeEntryComponent : ComponentBase
{
	[Parameter]
	public IncomeEntryModel Model { get; set; }

	[Parameter]
	public EventCallback<IncomeEntryModel> OnSave { get; set; }

	[Parameter]
	public EventCallback<IncomeEntryModel> OnDelete { get; set; }

	private void ToggleEdit()
	{
		if (Model.IsEditable)
		{
			OnSave.InvokeAsync(Model);
		}

		Model.IsEditable = !Model.IsEditable;
	}

	private void Delete()
	{
		OnDelete.InvokeAsync(Model);
		StateHasChanged();
	}
}