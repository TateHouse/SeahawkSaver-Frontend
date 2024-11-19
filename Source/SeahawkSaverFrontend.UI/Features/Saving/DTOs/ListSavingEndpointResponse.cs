namespace SeahawkSaverFrontend.UI.Features.Saving.DTOs;
public sealed record ListSavingEndpointResponse
{
	public required IReadOnlyList<ListSavingEndpointSavingResponse> Savings { get; init; }
}