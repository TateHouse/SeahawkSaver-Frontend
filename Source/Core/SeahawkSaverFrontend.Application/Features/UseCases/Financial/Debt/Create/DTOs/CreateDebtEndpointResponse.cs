namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Debt.Create.DTOs;
/**
 * <summary>
 * A data transfer object containing the data returned by the create debt endpoint response.
 * </summary>
 */
public sealed record CreateDebtEndpointResponse
{
	public required Guid DebtId { get; init; }
}