namespace SeahawkSaverFrontend.Application.Features.UseCases.User.Login.DTOs;
/**
 * <summary>
 * A data transfer object containing the user data provided in the login user endpoint request.
 * </summary>
 */
public sealed record LoginUserEndpointUserResponse
{
	public required Guid UserId { get; init; }
	public required string Email { get; init; }
	public required string FirstName { get; init; }
	public required string LastName { get; init; }
	public required bool IsAdmin { get; init; }
}