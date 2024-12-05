namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Saving.List;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Abstractions.UseCases.Financial;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Saving.List.DTOs;
using SeahawkSaverFrontend.Application.Utilities;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A use case for retrieving all savings for the authenticated user from the backend API.
 * </summary>
 */
public sealed class ListSavingModelUseCase : ListFinancialModelUseCase<SavingModel, ListSavingEndpointResponse>
{
	/**
	 * <summary>
	 * Instantiates a new <see cref="ListSavingModelUseCase"/> instance.
	 * </summary>
	 * <param name="httpClient">The <see cref="ApiHttpClient"/> to use.</param>
	 * <param name="userCache">The user cache.</param>
	 */
	public ListSavingModelUseCase(ApiHttpClient httpClient, IUserCache userCache)
		: base(httpClient, userCache)
	{

	}

	protected override string GetEndpointRelativePath()
	{
		return "saving/list";
	}

	protected override IEnumerable<SavingModel> MapResponse(ListSavingEndpointResponse response)
	{
		return response.Savings.Select(saving => new SavingModel
		{
			Id = saving.SavingId,
			Amount = saving.Amount,
			DateTime = saving.DateTime
		});
	}
}