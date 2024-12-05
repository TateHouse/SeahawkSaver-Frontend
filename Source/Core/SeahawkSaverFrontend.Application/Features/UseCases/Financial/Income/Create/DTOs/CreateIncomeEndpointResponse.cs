namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Income.Create.DTOs;
/**
 * <summary>
 * A data transfer object containing the data returned by the create income endpoint response.
 * </summary>
 */
public sealed record CreateIncomeEndpointResponse
{
	public required Guid IncomeId { get; init; }
}