using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Features.User.Manage.Components;
using SeahawkSaverFrontend.UI.Features.User.Manage.DTOs;

public partial class UserManagementComponent : ComponentBase
{
	private UserEntryModel model;

	protected override void OnInitialized()
	{
		model = new UserEntryModel
		{
			UserId = DataCache.User.UserId,
			Email = DataCache.User.Email,
			FirstName = DataCache.User.FirstName,
			LastName = DataCache.User.LastName,
			IsEditable = false
		};

		StateHasChanged();
	}

	private async Task Save(UserEntryModel model)
	{
		var result = await UserService.UpdateUserAsync(model);
		model.ErrorMessage = result ? null : "An error occurred when updating the user...";
		StateHasChanged();
	}
}