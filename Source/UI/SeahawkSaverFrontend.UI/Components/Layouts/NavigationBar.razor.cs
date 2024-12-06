using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Components.Layouts;
using SeahawkSaverFrontend.Application.Features.UseCases.User.Logout;

public partial class NavigationBar : ComponentBase, IDisposable
{
	protected override void OnInitialized()
	{
		AuthenticationCache.OnChange += StateHasChanged;
	}

	public void Dispose()
	{
		AuthenticationCache.OnChange -= StateHasChanged;
	}

	private void OnClick_Home()
	{
		NavigationManager.NavigateTo("/");
	}

	private void OnClick_Resources()
	{
		NavigationManager.NavigateTo("/resources");
	}

	private void OnClick_Logout()
	{
		var useCase = UseCaseFactory.Create<LogoutUserModelUseCase>();
		useCase.ExecuteAsync(null);

		NavigationManager.NavigateTo("/login");
	}
}