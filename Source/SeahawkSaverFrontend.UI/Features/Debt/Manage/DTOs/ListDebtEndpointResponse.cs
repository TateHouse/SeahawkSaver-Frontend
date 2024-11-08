namespace SeahawkSaverFrontend.UI.Features.Debt.Manage.DTOs;
public sealed record ListDebtEndpointResponse
{
	public required IReadOnlyList<ListDebtEndpointDebtResponse> Debts { get; init; }
}