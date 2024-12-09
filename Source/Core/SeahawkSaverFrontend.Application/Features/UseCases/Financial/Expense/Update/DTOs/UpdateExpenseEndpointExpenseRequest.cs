namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Expense.Update.DTOs;
/**
 * <summary>
 * A data transfer object containing the expense data provided in the update expense endpoint request.
 * </summary>
 */
public sealed record UpdateExpenseEndpointExpenseRequest
{
	public required Guid ExpenseId { get; init; }
	public required decimal Amount { get; init; }
	public required DateTime DateTime { get; init; }
}