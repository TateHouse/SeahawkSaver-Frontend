namespace SeahawkSaverFrontend.UI.Features.User.Manage.DTOs;
using SeahawkSaverFrontend.UI.Features.User.DTOs;

public sealed class UserEntryModel : UserModel
{
	public bool IsEditable { get; set; } = false;
	public string? ErrorMessage { get; set; }
}