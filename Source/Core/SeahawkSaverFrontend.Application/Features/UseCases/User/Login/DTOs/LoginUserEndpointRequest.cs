namespace SeahawkSaverFrontend.Application.Features.UseCases.User.Login.DTOs;
/**
 * <summary>
 * A data transfer object containing the user credential data provided in the login user endpoint request.
 * </summary>
 */
public sealed record LoginUserEndpointRequest
{
	public required string Email { get; init; }
	public required string Password { get; init; }
}