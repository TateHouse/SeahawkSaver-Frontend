using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Components.User.Manage;
using MudBlazor;
using SeahawkSaverFrontend.Application.Features.UseCases.User.List;
using SeahawkSaverFrontend.Domain.Models.User;

public partial class ManageUserModelsComponent : ComponentBase
{
	private List<UserModel> userModels = new List<UserModel>();

	[CascadingParameter]
	public MudDialogInstance Dialog { get; set; } = null!;

	protected override async Task OnInitializedAsync()
	{
		var useCase = UseCaseFactory.Create<ListUserModelUseCase>();
		userModels = (await useCase.ExecuteAsync(null)).ToList();

		StateHasChanged();
	}

	private async Task OnClick_EditAsync(UserModel userModel)
	{
		var parameters = new DialogParameters
		{
			{ "AllowIsActiveModification", true },
			{ "UserModel", userModel }
		};

		var options = new DialogOptions
		{
			FullWidth = true,
			MaxWidth = MaxWidth.Small
		};

		var dialog = await DialogService.ShowAsync<ManageUserModelFormComponent>("Profile", parameters, options);
		var dialogResult = await dialog.Result;

		if (!dialogResult.Canceled && dialogResult.Data is true)
		{
			StateHasChanged();
		}
	}
}