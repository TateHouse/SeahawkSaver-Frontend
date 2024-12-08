using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Components.Layouts;
using MudBlazor;
using SeahawkSaverFrontend.Application.Features.UseCases.User.Logout;
using SeahawkSaverFrontend.UI.Components.Financial.Report;
using SeahawkSaverFrontend.UI.Components.Financial.Report.DTOs;
using SeahawkSaverFrontend.UI.Components.User.Manage;

public partial class NavigationBar : ComponentBase, IDisposable
{
	private char userProfileCharacter;

	[CascadingParameter]
	public MudDialogInstance Dialog { get; set; } = null!;

	protected override void OnInitialized()
	{
		AuthenticationCache.OnChange += StateHasChanged;
		UserCache.OnChange += UpdateUserProfileCharacter;
	}

	public void Dispose()
	{
		AuthenticationCache.OnChange -= StateHasChanged;
		UserCache.OnChange -= UpdateUserProfileCharacter;
	}

	private void UpdateUserProfileCharacter()
	{
		if (!AuthenticationCache.IsAuthenticated() || UserCache.User.FirstName.Length <= 0)
		{
			return;
		}

		userProfileCharacter = UserCache.User.FirstName[0];
		StateHasChanged();
	}

	private void OnClick_Home()
	{
		NavigationManager.NavigateTo("/");
	}

	private void OnClick_Resources()
	{
		NavigationManager.NavigateTo("/resources");
	}

	private async Task OnClick_FinancialReportAsync()
	{
		var options = new DialogOptions
		{
			FullWidth = true,
			MaxWidth = MaxWidth.Small
		};

		var dialog = await DialogService.ShowAsync<FinancialReportFormComponent>("Generate Financial Report", options);
		var dialogResult = await dialog.Result;

		if (!dialogResult.Canceled && dialogResult.Data is true)
		{
			NavigationManager.NavigateTo("/financial-report");
		}
	}

	private async Task OnClick_ProfileAsync()
	{
		var parameters = new DialogParameters
		{
			{ "AllowIsActiveModification", false },
			{ "UserModel", UserCache.User }
		};

		var options = new DialogOptions
		{
			FullWidth = true,
			MaxWidth = MaxWidth.Small
		};

		await DialogService.ShowAsync<ManageUserModelFormComponent>("Profile", parameters, options);
	}

	private void OnClick_Logout()
	{
		var useCase = UseCaseFactory.Create<LogoutUserModelUseCase>();
		useCase.ExecuteAsync(null);

		NavigationManager.NavigateTo("/login");
	}
}