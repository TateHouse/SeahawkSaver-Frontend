namespace SeahawkSaverFrontend.UI.Features.Saving.Manage.DTOs;
using SeahawkSaverFrontend.UI.Features.Saving.DTOs;

public class SavingEntryModel : SavingModel
{
	public bool IsEditable { get; set; } = false;
	public string? ErrorMessage { get; set; }
}