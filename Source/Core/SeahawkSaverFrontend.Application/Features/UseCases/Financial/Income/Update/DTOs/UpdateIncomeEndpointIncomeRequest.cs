namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Income.Update.DTOs;
/**
 * <summary>
 * A data transfer object containing the income data provided in the update income endpoint request.
 * </summary>
 */
public sealed record UpdateIncomeEndpointIncomeRequest
{
	public required Guid IncomeId { get; init; }
	public required decimal Amount { get; init; }
	public required DateTime DateTime { get; init; }
}