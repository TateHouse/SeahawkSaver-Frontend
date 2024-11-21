using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Features.User.Manage.Components;
using SeahawkSaverFrontend.UI.Features.User.Manage.DTOs;

public partial class UserManagementComponent : ComponentBase
{
	private UserEntryModel model;
	private List<UserEntryModel> users = new List<UserEntryModel>();

	protected override async Task OnInitializedAsync()
	{
		model = new UserEntryModel
		{
			UserId = DataCache.User.UserId,
			Email = DataCache.User.Email,
			FirstName = DataCache.User.FirstName,
			LastName = DataCache.User.LastName,
			IsAdmin = DataCache.User.IsAdmin,
			IsActive = DataCache.User.IsActive,
			IsEditable = false
		};

		if (DataCache.User.IsAdmin)
		{
			users = (await UserService.GetUsersAsync())
					.Select(user => new UserEntryModel
					{
						UserId = user.UserId,
						Email = user.Email,
						FirstName = user.FirstName,
						LastName = user.LastName,
						IsAdmin = user.IsAdmin,
						IsActive = user.IsActive,
						IsEditable = false
					})
					.ToList();
		}

		StateHasChanged();
	}

	private async Task Save(UserEntryModel model)
	{
		var result = await UserService.UpdateUserAsync(model);
		model.ErrorMessage = result ? null : "An error occurred when updating the user...";
		StateHasChanged();
	}
}