namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Saving.Update;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Abstractions.UseCases.Financial;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Saving.Update.DTOs;
using SeahawkSaverFrontend.Application.Utilities;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A use case for updating a saving for the authenticated user in the backend API.
 * </summary>
 */
public sealed class UpdateSavingModelUseCase : UpdateFinancialModelUseCase<SavingModel, UpdateSavingEndpointRequest>
{
	/**
	 * <summary>
	 * Instantiates a new <see cref="UpdateSavingModelUseCase"/> instance.
	 * </summary>
	 * <param name="httpClient">The <see cref="ApiHttpClient"/> to use.</param>
	 * <param name="userCache">The user cache.</param>
	 * <param name="financialModelCache">The financial model cache.</param>
	 */
	public UpdateSavingModelUseCase(ApiHttpClient httpClient,
									IUserCache userCache,
									IFinancialModelCache<SavingModel> financialModelCache)
		: base(httpClient, userCache, financialModelCache)
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

	protected override UpdateSavingEndpointRequest MapRequest(SavingModel financialModel)
	{
		if (financialModel.DateTime == null)
		{
			throw new InvalidOperationException("The date must be provided.");
		}

		return new UpdateSavingEndpointRequest
		{
			Saving = new UpdateSavingEndpointSavingRequest
			{
				SavingId = financialModel.Id,
				Amount = financialModel.Amount,
				DateTime = financialModel.DateTime.Value
			}
		};
	}
}