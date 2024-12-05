namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Debt.Delete;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Abstractions.UseCases.Financial;
using SeahawkSaverFrontend.Application.Utilities;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A use case for deleting a debt for the authenticated user in the backend API.
 * </summary>
 */
public sealed class DeleteDebtModelUseCase : DeleteFinancialModelUseCase<DebtModel>
{
	/**
	 * <summary>
	 * Instantiates a new <see cref="DeleteDebtModelUseCase"/> instance.
	 * </summary>
	 * <param name="httpClient">The <see cref="ApiHttpClient"/> to use.</param>
	 * <param name="userCache">The user cache.</param>
	 */
	public DeleteDebtModelUseCase(ApiHttpClient httpClient, IUserCache userCache)
		: base(httpClient, userCache)
	{

	}

	protected override string GetEndpointRelativePath()
	{
		return "debt";
	}

	protected override string GetEndpointQueryParameters(DebtModel financialModel)
	{
		return $"debtId={financialModel.Id}";
	}
}