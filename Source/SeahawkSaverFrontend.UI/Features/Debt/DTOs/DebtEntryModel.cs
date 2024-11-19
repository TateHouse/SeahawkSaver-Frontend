namespace SeahawkSaverFrontend.UI.Features.Debt.DTOs;
public sealed class DebtEntryModel : DebtModel
{
	public bool IsEditable { get; set; } = false;
	public string? ErrorMessage { get; set; }
}