namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Expense.Delete;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Abstractions.UseCases.Financial;
using SeahawkSaverFrontend.Application.Utilities;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A use case for deleting an expense for the authenticated user in the backend API.
 * </summary>
 */
public sealed class DeleteExpenseModelUseCase : DeleteFinancialModelUseCase<ExpenseModel>
{
	/**
	 * <summary>
	 * Instantiates a new <see cref="DeleteExpenseModelUseCase"/> instance.
	 * </summary>
	 * <param name="httpClient">The <see cref="ApiHttpClient"/> to use.</param>
	 * <param name="userCache">The user cache.</param>
	 */
	public DeleteExpenseModelUseCase(ApiHttpClient httpClient, IUserCache userCache)
		: base(httpClient, userCache)
	{

	}

	protected override string GetEndpointRelativePath()
	{
		return "expense";
	}

	protected override string GetEndpointQueryParameters(ExpenseModel financialModel)
	{
		return $"expenseId={financialModel.Id}";
	}
}