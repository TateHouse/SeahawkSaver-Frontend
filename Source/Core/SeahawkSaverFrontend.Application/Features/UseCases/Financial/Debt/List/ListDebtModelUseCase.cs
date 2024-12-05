namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Debt.List;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Abstractions.UseCases.Financial;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Debt.List.DTOs;
using SeahawkSaverFrontend.Application.Utilities;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A use case for retrieving all debts for the authenticated user from the backend API.
 * </summary>
 */
public sealed class ListDebtModelUseCase : ListFinancialModelUseCase<DebtModel, ListDebtEndpointResponse>
{
	/**
	 * <summary>
	 * Instantiates a new <see cref="ListDebtModelUseCase"/> instance.
	 * </summary>
	 * <param name="httpClient">The <see cref="ApiHttpClient"/> to use.</param>
	 * <param name="userCache">The user cache.</param>
	 */
	public ListDebtModelUseCase(ApiHttpClient httpClient, IUserCache userCache)
		: base(httpClient, userCache)
	{

	}

	protected override string GetEndpointRelativePath()
	{
		return "debt/list";
	}

	protected override IEnumerable<DebtModel> MapResponse(ListDebtEndpointResponse response)
	{
		return response.Debts.Select(debt => new DebtModel
		{
			Id = debt.DebtId,
			Amount = debt.Amount,
			DateTime = debt.DateTime
		});
	}
}