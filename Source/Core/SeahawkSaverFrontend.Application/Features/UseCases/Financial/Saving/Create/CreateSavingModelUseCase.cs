namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Saving.Create;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Abstractions.UseCases.Financial;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Saving.Create.DTOs;
using SeahawkSaverFrontend.Application.Utilities;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A use case for creating a saving for the authenticated user in the backend API.
 * </summary>
 */
public sealed class CreateSavingModelUseCase : CreateFinancialModelUseCase<SavingModel, CreateSavingEndpointRequest, CreateSavingEndpointResponse>
{
	/**
	 * <summary>
	 * Instantiates a new <see cref="CreateSavingModelUseCase"/> instance.
	 * </summary>
	 * <param name="httpClient">The <see cref="ApiHttpClient"/> to use.</param>
	 * <param name="userCache">The user cache.</param>
	 */
	public CreateSavingModelUseCase(ApiHttpClient httpClient, IUserCache userCache)
		: base(httpClient, userCache)
	{

	}

	protected override string GetEndpointRelativePath()
	{
		return "saving";
	}

	protected override CreateSavingEndpointRequest MapRequest(SavingModel financialModel)
	{
		if (financialModel.DateTime == null)
		{
			throw new InvalidOperationException("The date must be provided.");
		}

		return new CreateSavingEndpointRequest
		{
			Saving = new CreateSavingEndpointSavingRequest
			{
				Amount = financialModel.Amount,
				DateTime = financialModel.DateTime.Value
			}
		};
	}

	protected override void MapId(SavingModel financialModel, CreateSavingEndpointResponse response)
	{
		financialModel.Id = response.SavingId;
	}
}