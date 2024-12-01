namespace SeahawkSaverFrontend.UI.Features.Expense.Services;
using SeahawkSaverFrontend.UI.Features.Expense.DTOs;

public interface IExpenseService
{
	public Task<IEnumerable<ExpenseModel>> GetExpensesAsync();

	public Task<bool> AddExpenseAsync(ExpenseModel model);
}