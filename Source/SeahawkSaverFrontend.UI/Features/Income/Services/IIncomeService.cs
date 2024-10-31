namespace SeahawkSaverFrontend.UI.Features.Income.Services;
using SeahawkSaverFrontend.UI.Features.Income.DTOs;
using SeahawkSaverFrontend.UI.Features.Income.Manage.DTOs;

public interface IIncomeService
{
	public Task<IEnumerable<IncomeModel>> GetIncomes();

	public Task<bool> AddIncome(IncomeModel model);

	public Task<bool> UpdateIncome(IncomeModel model);

	public Task<bool> RemoveIncome(IncomeModel model);
}