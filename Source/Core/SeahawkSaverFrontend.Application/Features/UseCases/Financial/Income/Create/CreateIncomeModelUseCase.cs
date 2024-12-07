namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Income.Create;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Abstractions.UseCases.Financial;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Income.Create.DTOs;
using SeahawkSaverFrontend.Application.Utilities;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A use case for creating an income for the authenticated user in the backend API.
 * </summary>
 */
public sealed class CreateIncomeModelUseCase : CreateFinancialModelUseCase<IncomeModel, CreateIncomeEndpointRequest, CreateIncomeEndpointResponse>
{
	/**
	 * <summary>
	 * Instantiates a new <see cref="CreateIncomeModelUseCase"/> instance.
	 * </summary>
	 * <param name="httpClient">The <see cref="ApiHttpClient"/> to use.</param>
	 * <param name="userCache">The user cache.</param>
	 * <param name="financialModelCache">The financial model cache.</param>
	 */
	public CreateIncomeModelUseCase(ApiHttpClient httpClient,
									IUserCache userCache,
									IFinancialModelCache<IncomeModel> financialModelCache)
		: base(httpClient, userCache, financialModelCache)
	{

	}

	protected override string GetEndpointRelativePath()
	{
		return "income";
	}

	protected override CreateIncomeEndpointRequest MapRequest(IncomeModel financialModel)
	{
		if (financialModel.DateTime == null)
		{
			throw new InvalidOperationException("The date must be provided.");
		}

		return new CreateIncomeEndpointRequest
		{
			Income = new CreateIncomeEndpointIncomeRequest
			{
				Amount = financialModel.Amount,
				DateTime = financialModel.DateTime.Value
			}
		};

	}

	protected override void MapId(IncomeModel financialModel, CreateIncomeEndpointResponse response)
	{
		financialModel.Id = response.IncomeId;
	}
}