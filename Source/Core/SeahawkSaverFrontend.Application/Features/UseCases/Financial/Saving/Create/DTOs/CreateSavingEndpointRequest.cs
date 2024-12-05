namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Saving.Create.DTOs;
/**
 * <summary>
 * A data transfer object containing the data provided in the create saving endpoint request.
 * </summary>
 */
public sealed record CreateSavingEndpointRequest
{
	public required CreateSavingEndpointSavingRequest Saving { get; init; }
}