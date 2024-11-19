namespace SeahawkSaverFrontend.UI.Features.Income.Services;
using SeahawkSaverFrontend.UI.Features.Income.DTOs;

public interface IIncomeService
{
	public Task<IEnumerable<IncomeModel>> GetIncomesAsync();

	public Task<bool> AddIncomeAsync(IncomeModel model);

	public Task<bool> UpdateIncomeAsync(IncomeModel model);

	public Task<bool> RemoveIncomeAsync(IncomeModel model);
}