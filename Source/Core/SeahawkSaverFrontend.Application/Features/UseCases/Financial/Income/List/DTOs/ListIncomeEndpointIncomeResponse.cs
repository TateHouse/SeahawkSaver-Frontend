namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Income.List.DTOs;
/**
 * <summary>
 * A data transfer object containing the debt data provided in the list income endpoint response.
 * </summary>
 */
public sealed record ListIncomeEndpointIncomeResponse
{
	public required Guid IncomeId { get; init; }
	public required decimal Amount { get; init; }
	public required DateTime DateTime { get; init; }
}