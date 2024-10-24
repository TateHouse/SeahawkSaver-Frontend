namespace SeahawkSaverFrontend.Application.Features.User.Commands.Login.DTOs;
public sealed record LoginUserEndpointResponse
{
	public required string Token { get; init; }
	public required LoginUserEndpointUserResponse User { get; init; }
}