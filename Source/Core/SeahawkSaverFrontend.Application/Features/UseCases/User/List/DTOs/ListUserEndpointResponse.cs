namespace SeahawkSaverFrontend.Application.Features.UseCases.User.List.DTOs;
/**
 * <summary>
 * A data transfer object containing the data returned by the list user endpoint response.
 * </summary>
 */
public sealed record ListUserEndpointResponse
{
	public required IReadOnlyList<ListUserEndpointUserResponse> Users { get; init; }
}