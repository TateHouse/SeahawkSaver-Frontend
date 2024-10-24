namespace SeahawkSaverFrontend.Application.Features.User.Commands.Login.DTOs;
public sealed record LoginUserEndpointUserResponse
{
	public required Guid UserId { get; init; }
	public required string Email { get; init; }
	public required string FirstName { get; init; }
	public required string LastName { get; init; }
}