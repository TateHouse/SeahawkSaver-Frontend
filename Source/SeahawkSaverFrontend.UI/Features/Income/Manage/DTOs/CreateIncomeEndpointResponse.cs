namespace SeahawkSaverFrontend.UI.Features.Income.Manage.DTOs;
public sealed record CreateIncomeEndpointResponse
{
	public required Guid IncomeId { get; init; }
}