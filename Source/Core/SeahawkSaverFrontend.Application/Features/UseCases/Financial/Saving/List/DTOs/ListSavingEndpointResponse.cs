namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Saving.List.DTOs;
/**
 * <summary>
 * A data transfer object containing the data returned by the list saving endpoint response.
 * </summary>
 */
public sealed record ListSavingEndpointResponse
{
	public required IReadOnlyList<ListSavingEndpointSavingResponse> Savings { get; init; }
}