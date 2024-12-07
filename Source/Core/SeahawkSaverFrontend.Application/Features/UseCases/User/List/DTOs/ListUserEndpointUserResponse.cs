namespace SeahawkSaverFrontend.Application.Features.UseCases.User.List.DTOs;
/**
 * <summary>
 * A data transfer object containing the user data provided in the list user endpoint response.
 * </summary>
 */
public sealed record ListUserEndpointUserResponse
{
	public required Guid UserId { get; init; }
	public required string Email { get; init; }
	public required string FirstName { get; init; }
	public required string LastName { get; init; }
	public required bool IsAdmin { get; init; }
	public required bool IsActive { get; init; }
}