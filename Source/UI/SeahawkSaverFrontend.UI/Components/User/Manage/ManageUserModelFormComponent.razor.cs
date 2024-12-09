namespace SeahawkSaverFrontend.UI.Components.User.Manage;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SeahawkSaverFrontend.Application.Features.UseCases.User.Update;
using SeahawkSaverFrontend.Domain.Models.User;

public partial class ManageUserModelFormComponent : ComponentBase
{
	private MudForm form = null!;

	[CascadingParameter]
	public MudDialogInstance Dialog { get; set; } = null!;

	[Parameter]
	public UserModel UserModel { get; set; } = null!;

	[Parameter]
	public bool AllowIsActiveModification { get; set; }

	private async Task OnClick_SaveAsync()
	{
		await form.Validate();

		if (!form.IsValid)
		{
			Snackbar.Add("Invalid form input.", Severity.Error);

			return;
		}

		var useCase = UseCaseFactory.Create<UpdateUserModelUseCase>();
		var response = await useCase.ExecuteAsync(UserModel);

		if (!response)
		{
			Snackbar.Add("User update failed, please try again.", Severity.Error);

			return;
		}

		Snackbar.Add("User update successful!", Severity.Success);
		Dialog.Close(true);
	}

	private void OnClick_Cancel()
	{
		Dialog.Close(false);
	}
}