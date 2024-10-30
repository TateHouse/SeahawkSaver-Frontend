namespace SeahawkSaverFrontend.UI.Features.User.Login.Components;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SeahawkSaverFrontend.UI.Features.User.Login.DTOs;

public partial class LoginFormComponent : ComponentBase
{
	private MudForm form = null!;
	private UserCredentialsModel model = new UserCredentialsModel();
	private string? errorMessage;

	private async Task LoginAsync()
	{
		await form.Validate();

		if (form.IsValid == false)
		{
			StateHasChanged();
			errorMessage = "Invalid input...";

			return;
		}

		var result = await LoginService.LoginAsync(model);

		if (result == false)
		{
			errorMessage = "Login failed... Please try again.";
			StateHasChanged();

			return;
		}

		NavigationManager.NavigateTo("/");
	}
}