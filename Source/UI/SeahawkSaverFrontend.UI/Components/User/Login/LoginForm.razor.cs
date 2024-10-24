using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Components.User.Login;
using MudBlazor;

public partial class LoginForm : ComponentBase
{
	private MudForm loginForm;
	private string email = null!;
	private string password = null!;
	private string? errorMessage;

	private async Task LoginAsync()
	{
		await loginForm.Validate();

		if (loginForm.IsValid == false)
		{
			return;
		}

		var result = await LoginService.LoginAsync(email, password);

		if (result == false)
		{
			errorMessage = "Login failed. Please try again...";
			StateHasChanged();

			return;
		}

		NavigationManager.NavigateTo("/");
	}
}