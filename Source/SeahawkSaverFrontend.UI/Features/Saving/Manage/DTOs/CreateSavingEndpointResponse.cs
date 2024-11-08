namespace SeahawkSaverFrontend.UI.Features.Saving.Manage.DTOs;
public sealed record CreateSavingEndpointResponse
{
	public required Guid SavingId { get; init; }
}