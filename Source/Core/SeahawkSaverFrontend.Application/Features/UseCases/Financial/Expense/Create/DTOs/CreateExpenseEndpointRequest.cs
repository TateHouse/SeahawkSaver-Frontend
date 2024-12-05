namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Expense.Create.DTOs;
/**
 * <summary>
 * A data transfer object containing the data provided in the create expense endpoint request.
 * </summary>
 */
public sealed record CreateExpenseEndpointRequest
{
	public required CreateExpenseEndpointExpenseRequest Expense { get; init; }
}