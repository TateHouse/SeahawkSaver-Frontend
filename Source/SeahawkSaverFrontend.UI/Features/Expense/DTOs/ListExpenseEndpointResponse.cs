namespace SeahawkSaverFrontend.UI.Features.Expense.DTOs;
public sealed record ListExpenseEndpointResponse
{
	public required IReadOnlyList<ListExpenseEndpointExpenseResponse> Expenses { get; init; }
}