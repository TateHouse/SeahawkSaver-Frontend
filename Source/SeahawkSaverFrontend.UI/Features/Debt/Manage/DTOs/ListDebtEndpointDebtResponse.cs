namespace SeahawkSaverFrontend.UI.Features.Debt.Manage.DTOs;
public class ListDebtEndpointDebtResponse
{
	public required Guid DebtId { get; init; }
	public required decimal Amount { get; init; }
	public required DateTime DateTime { get; init; }
}