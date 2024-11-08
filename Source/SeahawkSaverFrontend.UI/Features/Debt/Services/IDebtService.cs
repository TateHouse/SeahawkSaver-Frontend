namespace SeahawkSaverFrontend.UI.Features.Debt.Services;
using SeahawkSaverFrontend.UI.Features.Debt.DTOs;

public interface IDebtService
{
	public Task<IEnumerable<DebtModel>> GetDebtsAsync();

	public Task<bool> AddDebtAsync(DebtModel model);

	public Task<bool> UpdateDebtAsync(DebtModel model);

	public Task<bool> RemoveDebtAsync(DebtModel model);
}