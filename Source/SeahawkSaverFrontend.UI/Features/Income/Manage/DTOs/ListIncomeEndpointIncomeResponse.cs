namespace SeahawkSaverFrontend.UI.Features.Income.Manage.DTOs;
public sealed record ListIncomeEndpointIncomeResponse
{
	public required Guid IncomeId { get; init; }
	public required decimal Amount { get; init; }
	public required DateTime DateTime { get; init; }
}