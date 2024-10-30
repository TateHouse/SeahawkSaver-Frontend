namespace SeahawkSaverFrontend.UI.Layout;
using Microsoft.AspNetCore.Components;

public partial class NavMenu : ComponentBase, IDisposable
{
	protected override void OnInitialized()
	{
		DataCache.OnChange += StateHasChanged;
	}

	public void Dispose()
	{
		DataCache.OnChange -= StateHasChanged;
	}

	private void LogOut()
	{
		LogOutService.LogOut();
		NavigationManager.NavigateTo("/login");
	}
}