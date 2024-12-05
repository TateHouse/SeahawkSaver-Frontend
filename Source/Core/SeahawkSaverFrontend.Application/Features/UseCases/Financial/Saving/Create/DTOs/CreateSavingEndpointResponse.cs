namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Saving.Create.DTOs;
/**
 * <summary>
 * A data transfer object containing the data returned by the create saving endpoint response.
 * </summary>
 */
public sealed record CreateSavingEndpointResponse
{
	public required Guid SavingId { get; init; }
}