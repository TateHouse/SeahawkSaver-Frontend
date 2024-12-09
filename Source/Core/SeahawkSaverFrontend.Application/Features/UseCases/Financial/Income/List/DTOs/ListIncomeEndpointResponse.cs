namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Income.List.DTOs;
/**
 * <summary>
 * A data transfer object containing the data returned by the list income endpoint response.
 * </summary>
 */
public sealed record ListIncomeEndpointResponse
{
	public required IReadOnlyList<ListIncomeEndpointIncomeResponse> Incomes { get; init; }
}