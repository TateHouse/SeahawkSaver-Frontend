namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Debt.Update;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Abstractions.UseCases.Financial;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Debt.Update.DTOs;
using SeahawkSaverFrontend.Application.Utilities;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A use case for updating a debt for the authenticated user in the backend API.
 * </summary>
 */
public sealed class UpdateDebtModelUseCase : UpdateFinancialModelUseCase<DebtModel, UpdateDebtEndpointRequest>
{
	/**
	 * <summary>
	 * Instantiates a new <see cref="UpdateDebtModelUseCase"/> instance.
	 * </summary>
	 * <param name="httpClient">The <see cref="ApiHttpClient"/> to use.</param>
	 * <param name="userCache">The user cache.</param>
	 * <param name="financialModelCache">The financial model cache.</param>
	 */
	public UpdateDebtModelUseCase(ApiHttpClient httpClient,
								  IUserCache userCache,
								  IFinancialModelCache<DebtModel> financialModelCache)
		: base(httpClient, userCache, financialModelCache)
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

	protected override UpdateDebtEndpointRequest MapRequest(DebtModel financialModel)
	{
		if (financialModel.DateTime == null)
		{
			throw new InvalidOperationException("The date must be provided.");
		}

		return new UpdateDebtEndpointRequest
		{
			Debt = new UpdateDebtEndpointDebtRequest
			{
				DebtId = financialModel.Id,
				Amount = financialModel.Amount,
				DateTime = financialModel.DateTime.Value
			}
		};
	}
}