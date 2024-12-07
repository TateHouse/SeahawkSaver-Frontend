namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Income.Update;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Abstractions.UseCases.Financial;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Income.Update.DTOs;
using SeahawkSaverFrontend.Application.Utilities;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A use case for updating an income for the authenticated user in the backend API.
 * </summary>
 */
public sealed class UpdateIncomeModelUseCase : UpdateFinancialModelUseCase<IncomeModel, UpdateIncomeEndpointRequest>
{
	/**
	 * <summary>
	 * Instantiates a new <see cref="UpdateIncomeModelUseCase"/> instance.
	 * </summary>
	 * <param name="httpClient">The <see cref="ApiHttpClient"/> to use.</param>
	 * <param name="userCache">The user cache.</param>
	 * <param name="financialModelCache">The financial model cache.</param>
	 */
	public UpdateIncomeModelUseCase(ApiHttpClient httpClient,
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

	protected override UpdateIncomeEndpointRequest MapRequest(IncomeModel financialModel)
	{
		if (financialModel.DateTime == null)
		{
			throw new InvalidOperationException("The date must be provided.");
		}

		return new UpdateIncomeEndpointRequest
		{
			Income = new UpdateIncomeEndpointIncomeRequest
			{
				IncomeId = financialModel.Id,
				Amount = financialModel.Amount,
				DateTime = financialModel.DateTime.Value
			}
		};
	}
}