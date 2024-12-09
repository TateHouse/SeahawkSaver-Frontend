namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Expense.List.DTOs;
/**
 * <summary>
 * A data transfer object containing the data returned by the list expense endpoint response.
 * </summary>
 */
public sealed record ListExpenseEndpointResponse
{
	public required IReadOnlyList<ListExpenseEndpointExpenseResponse> Expenses { get; init; }
}