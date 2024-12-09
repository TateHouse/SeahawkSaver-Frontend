namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Debt.Update.DTOs;
/**
 * <summary>
 * A data transfer object containing the data provided in the update debt endpoint request.
 * </summary>
 */
public sealed record UpdateDebtEndpointRequest
{
	public required UpdateDebtEndpointDebtRequest Debt { get; init; }
}