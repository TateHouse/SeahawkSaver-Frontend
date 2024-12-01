namespace SeahawkSaverFrontend.UI.Features.Expense.DTOs;
public sealed class  ExpenseEntryModel : ExpenseModel
{
	public bool IsEditable { get; set; } = false;
	public string? ErrorMessage { get; set; }
}