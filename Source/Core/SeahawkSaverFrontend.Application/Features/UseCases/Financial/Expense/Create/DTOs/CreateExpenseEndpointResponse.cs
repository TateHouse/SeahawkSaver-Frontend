namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Expense.Create.DTOs;
/**
 * <summary>
 * A data transfer object containing the data returned by the create expense endpoint response.
 * </summary>
 */
public sealed record CreateExpenseEndpointResponse
{
	public required Guid ExpenseId { get; init; }
}