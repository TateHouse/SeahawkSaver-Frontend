namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Expense.Create.DTOs;
/**
 * <summary>
 * A data transfer object containing the expense data provided in the create expense endpoint request.
 * </summary>
 */
public sealed record CreateExpenseEndpointExpenseRequest
{
	public required decimal Amount { get; init; }
	public required DateTime DateTime { get; init; }
}