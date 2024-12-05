namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Income.Update.DTOs;
/**
 * <summary>
 * A data transfer object containing the data provided in the update income endpoint request.
 * </summary>
 */
public sealed record UpdateIncomeEndpointRequest
{
	public required UpdateIncomeEndpointIncomeRequest Income { get; init; }
}