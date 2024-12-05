namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Income.Create.DTOs;
/**
 * <summary>
 * A data transfer object containing the data provided in the create income endpoint request.
 * </summary>
 */
public sealed record CreateIncomeEndpointRequest
{
	public required CreateIncomeEndpointIncomeRequest Income { get; init; }
}