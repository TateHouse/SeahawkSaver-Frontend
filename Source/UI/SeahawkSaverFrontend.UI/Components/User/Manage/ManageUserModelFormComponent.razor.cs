namespace SeahawkSaverFrontend.UI.Components.User.Manage;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SeahawkSaverFrontend.Application.Features.UseCases.User.Update;
using SeahawkSaverFrontend.Domain.Models.User;

public partial class ManageUserModelFormComponent : ComponentBase
{
	private MudForm form = null!;
	private readonly UserModel userModel = new UserModel();

	[CascadingParameter]
	public MudDialogInstance Dialog { get; set; } = null!;

	[Parameter]
	public bool AllowIsActiveModification { get; set; } = false;

	protected override void OnInitialized()
	{
		userModel.UserId = UserCache.User.UserId;
		userModel.Email = UserCache.User.Email;
		userModel.FirstName = UserCache.User.FirstName;
		userModel.LastName = UserCache.User.LastName;
		userModel.IsAdmin = UserCache.User.IsAdmin;
		userModel.IsActive = UserCache.User.IsActive;
	}

	private async Task OnClick_Save()
	{
		await form.Validate();

		if (!form.IsValid)
		{
			Snackbar.Add("Invalid form input.", Severity.Error);

			return;
		}

		var useCase = UseCaseFactory.Create<UpdateUserModelUseCase>();
		var response = await useCase.ExecuteAsync(userModel);

		if (!response)
		{
			Snackbar.Add("User update failed, please try again.", Severity.Error);

			return;
		}

		Snackbar.Add("User update successful!", Severity.Success);
		Dialog.Close();
	}

	private void OnClick_Cancel()
	{
		Dialog.Close();
	}
}