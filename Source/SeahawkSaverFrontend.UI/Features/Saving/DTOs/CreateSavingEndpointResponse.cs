namespace SeahawkSaverFrontend.UI.Features.Saving.DTOs;
public sealed record CreateSavingEndpointResponse
{
	public required Guid SavingId { get; init; }
}