namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Debt.Update.DTOs;
/**
 * <summary>
 * A data transfer object containing the debt data provided in the update debt endpoint request.
 * </summary>
 */
public sealed record UpdateDebtEndpointDebtRequest
{
	public required Guid DebtId { get; init; }
	public required decimal Amount { get; init; }
	public required DateTime DateTime { get; init; }
}