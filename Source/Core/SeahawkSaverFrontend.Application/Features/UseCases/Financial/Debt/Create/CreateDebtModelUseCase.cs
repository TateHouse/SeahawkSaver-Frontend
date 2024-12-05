namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Debt.Create;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Abstractions.UseCases.Financial;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Debt.Create.DTOs;
using SeahawkSaverFrontend.Application.Utilities;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A use case for creating a debt for the authenticated user in the backend API.
 * </summary>
 */
public sealed class CreateDebtModelUseCase : CreateFinancialModelUseCase<DebtModel, CreateDebtEndpointRequest, CreateDebtEndpointResponse>
{
	/**
	 * <summary>
	 * Instantiates a new <see cref="CreateDebtModelUseCase"/> instance.
	 * </summary>
	 * <param name="httpClient">The <see cref="ApiHttpClient"/> to use.</param>
	 * <param name="userCache">The user cache.</param>
	 */
	public CreateDebtModelUseCase(ApiHttpClient httpClient, IUserCache userCache)
		: base(httpClient, userCache)
	{

	}

	protected override string GetEndpointRelativePath()
	{
		return "debt";
	}

	protected override CreateDebtEndpointRequest MapRequest(DebtModel financialModel)
	{
		if (financialModel.DateTime == null)
		{
			throw new InvalidOperationException("The date must be provided.");
		}

		return new CreateDebtEndpointRequest
		{
			Debt = new CreateDebtEndpointDebtRequest
			{
				Amount = financialModel.Amount,
				DateTime = financialModel.DateTime.Value
			}
		};
	}

	protected override void MapId(DebtModel financialModel, CreateDebtEndpointResponse response)
	{
		financialModel.Id = response.DebtId;
	}
}