namespace SeahawkSaverFrontend.Application.Features.UseCases.User.Update.DTOs;
/**
 * <summary>
 * A data transfer object containing the user data provided in the update user endpoint request.
 * </summary>
 */
public sealed record UpdateUserEndpointUserRequest
{
	public required string Email { get; init; }
	public required string FirstName { get; init; }
	public required string LastName { get; init; }
	public required bool IsActive { get; init; }
}