namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Debt.Create.DTOs;
/**
 * <summary>
 * A data transfer object containing the data provided in the create debt endpoint request.
 * </summary>
 */
public sealed record CreateDebtEndpointRequest
{
	public required CreateDebtEndpointDebtRequest Debt { get; init; }
}