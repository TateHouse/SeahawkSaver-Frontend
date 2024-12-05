namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Expense.Create;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Abstractions.UseCases.Financial;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Expense.Create.DTOs;
using SeahawkSaverFrontend.Application.Utilities;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A use case for creating an expense for the authenticated user in the backend API.
 * </summary>
 */
public sealed class CreateExpenseModelUseCase : CreateFinancialModelUseCase<ExpenseModel, CreateExpenseEndpointRequest, CreateExpenseEndpointResponse>
{
	/**
	 * <summary>
	 * Instantiates a new <see cref="CreateExpenseModelUseCase"/> instance.
	 * </summary>
	 * <param name="httpClient">The <see cref="ApiHttpClient"/> to use.</param>
	 * <param name="userCache">The user cache.</param>
	 */
	public CreateExpenseModelUseCase(ApiHttpClient httpClient, IUserCache userCache)
		: base(httpClient, userCache)
	{

	}

	protected override string GetEndpointRelativePath()
	{
		return "expense";
	}

	protected override CreateExpenseEndpointRequest MapRequest(ExpenseModel financialModel)
	{
		if (financialModel.DateTime == null)
		{
			throw new InvalidOperationException("The date must be provided.");
		}

		return new CreateExpenseEndpointRequest
		{
			Expense = new CreateExpenseEndpointExpenseRequest
			{
				Amount = financialModel.Amount,
				DateTime = financialModel.DateTime.Value
			}
		};
	}

	protected override void MapId(ExpenseModel financialModel, CreateExpenseEndpointResponse response)
	{
		financialModel.Id = response.ExpenseId;
	}
}