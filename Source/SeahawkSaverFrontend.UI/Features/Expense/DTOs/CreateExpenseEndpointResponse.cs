namespace SeahawkSaverFrontend.UI.Features.Expense.DTOs;
public sealed record CreateExpenseEndpointResponse
{
	public required Guid ExpenseId { get; init; }
}