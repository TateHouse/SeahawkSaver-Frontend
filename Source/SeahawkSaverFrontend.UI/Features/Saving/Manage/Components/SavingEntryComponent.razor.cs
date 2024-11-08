using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Features.Saving.Manage.Components;
using SeahawkSaverFrontend.UI.Features.Saving.Manage.DTOs;

public partial class SavingEntryComponent : ComponentBase
{
	[Parameter]
	public SavingEntryModel Model { get; set; }

	[Parameter]
	public EventCallback<SavingEntryModel> OnSave { get; set; }

	[Parameter]
	public EventCallback<SavingEntryModel> OnDelete { get; set; }

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