namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Saving.Delete;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Abstractions.UseCases.Financial;
using SeahawkSaverFrontend.Application.Utilities;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A use case for deleting a saving for the authenticated user in the backend API.
 * </summary>
 */
public sealed class DeleteSavingModelUseCase : DeleteFinancialModelUseCase<SavingModel>
{
	/**
	 * <summary>
	 * Instantiates a new <see cref="DeleteSavingModelUseCase"/> instance.
	 * </summary>
	 * <param name="httpClient">The <see cref="ApiHttpClient"/> to use.</param>
	 * <param name="userCache">The user cache.</param>
	 */
	public DeleteSavingModelUseCase(ApiHttpClient httpClient, IUserCache userCache)
		: base(httpClient, userCache)
	{

	}

	protected override string GetEndpointRelativePath()
	{
		return "saving";
	}

	protected override string GetEndpointQueryParameters(SavingModel financialModel)
	{
		return $"savingId={financialModel.Id}";
	}
}