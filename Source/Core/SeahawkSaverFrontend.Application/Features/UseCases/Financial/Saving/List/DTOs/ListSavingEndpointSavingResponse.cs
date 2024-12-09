namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Saving.List.DTOs;
/**
 * <summary>
 * A data transfer object containing the debt data provided in the list saving endpoint response.
 * </summary>
 */
public sealed record ListSavingEndpointSavingResponse
{
	public required Guid SavingId { get; init; }
	public required decimal Amount { get; init; }
	public required DateTime DateTime { get; init; }
}