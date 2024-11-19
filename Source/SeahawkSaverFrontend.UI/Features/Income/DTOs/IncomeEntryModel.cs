namespace SeahawkSaverFrontend.UI.Features.Income.DTOs;
public sealed class IncomeEntryModel : IncomeModel
{
	public bool IsEditable { get; set; } = false;
	public string? ErrorMessage { get; set; }
}