namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Debt.Create.DTOs;
/**
 * <summary>
 * A data transfer object containing the debt data provided in the create debt endpoint request.
 * </summary>
 */
public sealed record CreateDebtEndpointDebtRequest
{
	public required decimal Amount { get; init; }
	public required DateTime DateTime { get; init; }
}