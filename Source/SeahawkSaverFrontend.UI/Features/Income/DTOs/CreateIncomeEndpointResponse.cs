namespace SeahawkSaverFrontend.UI.Features.Income.DTOs;
public sealed record CreateIncomeEndpointResponse
{
	public required Guid IncomeId { get; init; }
}