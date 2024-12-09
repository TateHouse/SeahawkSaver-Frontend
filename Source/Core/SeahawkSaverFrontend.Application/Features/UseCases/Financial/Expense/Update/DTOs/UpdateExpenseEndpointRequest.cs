namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Expense.Update.DTOs;
/**
 * <summary>
 * A data transfer object containing the data provided in the update expense endpoint request.
 * </summary>
 */
public sealed record UpdateExpenseEndpointRequest
{
	public required UpdateExpenseEndpointExpenseRequest Expense { get; init; }
}