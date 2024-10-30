namespace SeahawkSaverFrontend.UI.Features.Income.Services;
using SeahawkSaverFrontend.UI.Features.Income.DTOs;

public interface IIncomeService
{
	public Task<IEnumerable<IncomeModel>> GetIncomes();

	public Task<bool> UpdateIncome(IncomeModel model);

	public Task<bool> RemoveIncome(IncomeModel model);
}