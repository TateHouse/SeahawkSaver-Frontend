using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Components.User.Login;
using MudBlazor;
using SeahawkSaverFrontend.Application.Features.UseCases.User.Login;
using SeahawkSaverFrontend.Domain.Models.User;

public partial class LoginUserFormComponent : ComponentBase
{
	private MudForm form = null!;
	private bool isPasswordVisible;
	private InputType passwordInputType = InputType.Password;
	private string passwordInputIcon = Icons.Material.Filled.VisibilityOff;
	private readonly UserCredentialsModel userCredentialsModel = new UserCredentialsModel();

	private void TogglePasswordVisibility()
	{
		if (isPasswordVisible)
		{
			isPasswordVisible = false;
			passwordInputIcon = Icons.Material.Filled.VisibilityOff;
			passwordInputType = InputType.Password;
		}
		else
		{
			isPasswordVisible = true;
			passwordInputIcon = Icons.Material.Filled.Visibility;
			passwordInputType = InputType.Text;
		}
	}

	private async Task OnClick_LoginAsync()
	{
		await form.Validate();

		if (!form.IsValid)
		{
			Snackbar.Add("Invalid login input, please try again.", Severity.Error);

			return;
		}

		var useCase = UseCaseFactory.Create<LoginUserModelUseCase>();
		var response = await useCase.ExecuteAsync(userCredentialsModel);

		if (!response)
		{
			Snackbar.Add("Login failed, please try again.", Severity.Error);

			return;
		}

		NavigationManager.NavigateTo("/");
	}
}