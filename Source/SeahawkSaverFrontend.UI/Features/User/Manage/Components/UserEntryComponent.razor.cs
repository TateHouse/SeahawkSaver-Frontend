using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Features.User.Manage.Components;
using SeahawkSaverFrontend.UI.Features.User.Manage.DTOs;

public partial class UserEntryComponent : ComponentBase
{
	[Parameter]
	public UserEntryModel Model { get; set; }

	[Parameter]
	public EventCallback<UserEntryModel> OnSave { get; set; }

	private void ToggleEdit()
	{
		if (Model.IsEditable)
		{
			OnSave.InvokeAsync(Model);
		}

		Model.IsEditable = !Model.IsEditable;
	}
}