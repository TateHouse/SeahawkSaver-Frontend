namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Saving.Update.DTOs;
/**
 * <summary>
 * A data transfer object containing the saving data provided in the update saving endpoint request.
 * </summary>
 */
public sealed record UpdateSavingEndpointSavingRequest
{
	public required Guid SavingId { get; init; }
	public required decimal Amount { get; init; }
	public required DateTime DateTime { get; init; }
}