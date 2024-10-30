namespace SeahawkSaverFrontend.UI.Features.Income.Manage.DTOs;
public sealed record ListIncomeEndpointResponse
{
	public required IReadOnlyList<ListIncomeEndpointIncomeResponse> Incomes { get; init; }
}