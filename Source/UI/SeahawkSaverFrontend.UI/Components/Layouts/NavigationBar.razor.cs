using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Components.Layouts;
using SeahawkSaverFrontend.Application.Features.UseCases.User.Logout;

public partial class NavigationBar : ComponentBase, IDisposable
{
	private char userProfileCharacter;

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

	private void OnClick_Profile()
	{
		NavigationManager.NavigateTo("/profile");
	}

	private void OnClick_Logout()
	{
		var useCase = UseCaseFactory.Create<LogoutUserModelUseCase>();
		useCase.ExecuteAsync(null);

		NavigationManager.NavigateTo("/login");
	}
}