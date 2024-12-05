namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Income.List;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Abstractions.UseCases.Financial;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Income.List.DTOs;
using SeahawkSaverFrontend.Application.Utilities;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A use case for retrieving all incomes for the authenticated user from the backend API.
 * </summary>
 */
public sealed class ListIncomeModelUseCase : ListFinancialModelUseCase<IncomeModel, ListIncomeEndpointResponse>
{
	/**
	 * <summary>
	 * Instantiates a new <see cref="ListIncomeModelUseCase"/> instance.
	 * </summary>
	 * <param name="httpClient">The <see cref="ApiHttpClient"/> to use.</param>
	 * <param name="userCache">The user cache.</param>
	 */
	public ListIncomeModelUseCase(ApiHttpClient httpClient, IUserCache userCache)
		: base(httpClient, userCache)
	{

	}

	protected override string GetEndpointRelativePath()
	{
		return "income/list";
	}

	protected override IEnumerable<IncomeModel> MapResponse(ListIncomeEndpointResponse response)
	{
		return response.Incomes.Select(income => new IncomeModel
		{
			Id = income.IncomeId,
			Amount = income.Amount,
			DateTime = income.DateTime
		});
	}
}