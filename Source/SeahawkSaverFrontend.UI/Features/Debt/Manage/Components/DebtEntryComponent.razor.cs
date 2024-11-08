using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Features.Debt.Manage.Components;
using SeahawkSaverFrontend.UI.Features.Debt.Manage.DTOs;

public partial class DebtEntryComponent : ComponentBase
{
	[Parameter]
	public DebtEntryModel Model { get; set; }

	[Parameter]
	public EventCallback<DebtEntryModel> OnSave { get; set; }

	[Parameter]
	public EventCallback<DebtEntryModel> OnDelete { get; set; }

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