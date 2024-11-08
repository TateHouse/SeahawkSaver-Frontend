namespace SeahawkSaverFrontend.UI.Features.Saving.Manage.DTOs;
public sealed record ListSavingEndpointResponse
{
	public required IReadOnlyList<ListSavingEndpointSavingResponse> Savings { get; init; }
}