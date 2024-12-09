namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Saving.Update.DTOs;
/**
 * <summary>
 * A data transfer object containing the data provided in the update saving endpoint request.
 * </summary>
 */
public sealed record UpdateSavingEndpointRequest
{
	public required UpdateSavingEndpointSavingRequest Saving { get; init; }
}