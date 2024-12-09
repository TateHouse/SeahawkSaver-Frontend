namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Income.Create.DTOs;
/**
 * <summary>
 * A data transfer object containing the income data provided in the create income endpoint request.
 * </summary>
 */
public sealed record CreateIncomeEndpointIncomeRequest
{
	public required decimal Amount { get; init; }
	public required DateTime DateTime { get; init; }
}