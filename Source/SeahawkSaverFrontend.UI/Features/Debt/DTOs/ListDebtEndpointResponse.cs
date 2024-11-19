namespace SeahawkSaverFrontend.UI.Features.Debt.DTOs;
public sealed record ListDebtEndpointResponse
{
	public required IReadOnlyList<ListDebtEndpointDebtResponse> Debts { get; init; }
}