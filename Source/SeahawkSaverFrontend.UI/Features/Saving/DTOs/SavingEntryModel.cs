namespace SeahawkSaverFrontend.UI.Features.Saving.DTOs;
public sealed class SavingEntryModel : SavingModel
{
	public bool IsEditable { get; set; } = false;
	public string? ErrorMessage { get; set; }
}