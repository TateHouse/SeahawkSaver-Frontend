namespace SeahawkSaverFrontend.UI.Features.Expense.DTOs;
public sealed record ListExpenseEndpointExpenseResponse
{
	public required Guid ExpenseId { get; init; }
	public required decimal Amount { get; init; }
	public required DateTime DateTime { get; init; }
}