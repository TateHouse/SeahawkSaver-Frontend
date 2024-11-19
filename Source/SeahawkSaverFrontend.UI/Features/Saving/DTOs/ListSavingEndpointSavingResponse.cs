namespace SeahawkSaverFrontend.UI.Features.Saving.DTOs;
public sealed record ListSavingEndpointSavingResponse
{
	public required Guid SavingId { get; init; }
	public required decimal Amount { get; init; }
	public required DateTime DateTime { get; init; }
}