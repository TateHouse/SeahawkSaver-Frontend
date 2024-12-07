namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Expense.Update;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Abstractions.UseCases.Financial;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Expense.Update.DTOs;
using SeahawkSaverFrontend.Application.Utilities;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A use case for updating an expense for the authenticated user in the backend API.
 * </summary>
 */
public sealed class UpdateExpenseModelUseCase : UpdateFinancialModelUseCase<ExpenseModel, UpdateExpenseEndpointRequest>
{
	/**
	 * <summary>
	 * Instantiates a new <see cref="UpdateExpenseModelUseCase"/> instance.
	 * </summary>
	 * <param name="httpClient">The <see cref="ApiHttpClient"/> to use.</param>
	 * <param name="userCache">The user cache.</param>
	 * <param name="financialModelCache">The financial model cache.</param>
	 */
	public UpdateExpenseModelUseCase(ApiHttpClient httpClient,
									 IUserCache userCache,
									 IFinancialModelCache<ExpenseModel> financialModelCache)
		: base(httpClient, userCache, financialModelCache)
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

	protected override UpdateExpenseEndpointRequest MapRequest(ExpenseModel financialModel)
	{
		if (financialModel.DateTime == null)
		{
			throw new InvalidOperationException("The date must be provided.");
		}

		return new UpdateExpenseEndpointRequest
		{
			Expense = new UpdateExpenseEndpointExpenseRequest
			{
				ExpenseId = financialModel.Id,
				Amount = financialModel.Amount,
				DateTime = financialModel.DateTime.Value
			}
		};
	}
}