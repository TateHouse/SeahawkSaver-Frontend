namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Expense.List;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Abstractions.UseCases.Financial;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Expense.List.DTOs;
using SeahawkSaverFrontend.Application.Utilities;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A use case for retrieving all expenses for the authenticated user from the backend API.
 * </summary>
 */
public sealed class ListExpenseModelUseCase : ListFinancialModelUseCase<ExpenseModel, ListExpenseEndpointResponse>
{
	/**
	 * <summary>
	 * Instantiates a new <see cref="ListExpenseModelUseCase"/> instance.
	 * </summary>
	 * <param name="httpClient">The <see cref="ApiHttpClient"/> to use.</param>
	 * <param name="userCache">The user cache.</param>
	 */
	public ListExpenseModelUseCase(ApiHttpClient httpClient, IUserCache userCache)
		: base(httpClient, userCache)
	{

	}

	protected override string GetEndpointRelativePath()
	{
		return "expense/list";
	}

	protected override IEnumerable<ExpenseModel> MapResponse(ListExpenseEndpointResponse response)
	{
		return response.Expenses.Select(expense => new ExpenseModel
		{
			Id = expense.ExpenseId,
			Amount = expense.Amount,
			DateTime = expense.DateTime
		});
	}
}