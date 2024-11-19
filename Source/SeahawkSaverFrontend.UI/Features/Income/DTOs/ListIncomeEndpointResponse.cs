namespace SeahawkSaverFrontend.UI.Features.Income.DTOs;
public sealed record ListIncomeEndpointResponse
{
	public required IReadOnlyList<ListIncomeEndpointIncomeResponse> Incomes { get; init; }
}