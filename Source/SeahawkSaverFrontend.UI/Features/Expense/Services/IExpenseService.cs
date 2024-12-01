namespace SeahawkSaverFrontend.UI.Features.Expense.Services;
using SeahawkSaverFrontend.UI.Features.Expense.DTOs;

public interface IExpenseService
{
	public Task<IEnumerable<ExpenseModel>> GetExpensesAsync();

	public Task<bool> AddExpenseAsync(ExpenseModel model);

	public Task<bool> UpdateExpenseAsync(ExpenseModel model);

	public Task<bool> RemoveExpenseAsync(ExpenseModel model);
}