namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Expense.List.DTOs;
/**
 * <summary>
 * A data transfer object containing the debt data provided in the list expense endpoint response.
 * </summary>
 */
public sealed record ListExpenseEndpointExpenseResponse
{
	public required Guid ExpenseId { get; init; }
	public required decimal Amount { get; init; }
	public required DateTime DateTime { get; init; }
}