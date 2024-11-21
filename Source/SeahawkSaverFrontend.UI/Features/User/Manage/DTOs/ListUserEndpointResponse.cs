namespace SeahawkSaverFrontend.UI.Features.User.Manage.DTOs;
public sealed record ListUserEndpointResponse
{
	public required IReadOnlyList<ListUserEndpointUserResponse> Users { get; init; }
}