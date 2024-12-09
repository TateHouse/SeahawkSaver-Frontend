namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Debt.List.DTOs;
/**
 * <summary>
 * A data transfer object containing the debt data provided in the list debt endpoint response.
 * </summary>
 */
public sealed record ListDebtEndpointDebtResponse
{
	public required Guid DebtId { get; init; }
	public required decimal Amount { get; init; }
	public required DateTime DateTime { get; init; }
}