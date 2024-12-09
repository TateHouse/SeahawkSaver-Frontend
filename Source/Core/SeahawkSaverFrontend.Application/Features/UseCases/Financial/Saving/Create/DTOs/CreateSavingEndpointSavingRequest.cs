namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Saving.Create.DTOs;
/**
 * <summary>
 * A data transfer object containing the saving data provided in the create saving endpoint request.
 * </summary>
 */
public sealed record CreateSavingEndpointSavingRequest
{
	public required decimal Amount { get; init; }
	public required DateTime DateTime { get; init; }
}