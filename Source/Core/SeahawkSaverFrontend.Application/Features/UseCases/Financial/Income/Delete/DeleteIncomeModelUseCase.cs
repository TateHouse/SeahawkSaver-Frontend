namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Income.Delete;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Abstractions.UseCases.Financial;
using SeahawkSaverFrontend.Application.Utilities;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A use case for deleting an income for the authenticated user in the backend API.
 * </summary>
 */
public sealed class DeleteIncomeModelUseCase : DeleteFinancialModelUseCase<IncomeModel>
{
	/**
	 * <summary>
	 * Instantiates a new <see cref="DeleteIncomeModelUseCase"/> instance.
	 * </summary>
	 * <param name="httpClient">The <see cref="ApiHttpClient"/> to use.</param>
	 * <param name="userCache">The user cache.</param>
	 * <param name="financialModelCache">The financial model cache.</param>
	 */
	public DeleteIncomeModelUseCase(ApiHttpClient httpClient,
									IUserCache userCache,
									IFinancialModelCache<IncomeModel> financialModelCache)
		: base(httpClient, userCache, financialModelCache)
	{

	}

	protected override string GetEndpointRelativePath()
	{
		return "income";
	}

	protected override string GetEndpointQueryParameters(IncomeModel financialModel)
	{
		return $"incomeId={financialModel.Id}";
	}
}