namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Debt.List.DTOs;
/**
 * <summary>
 * A data transfer object containing the data returned by the list debt endpoint response.
 * </summary>
 */
public sealed record ListDebtEndpointResponse
{
	public required IReadOnlyList<ListDebtEndpointDebtResponse> Debts { get; init; }
}