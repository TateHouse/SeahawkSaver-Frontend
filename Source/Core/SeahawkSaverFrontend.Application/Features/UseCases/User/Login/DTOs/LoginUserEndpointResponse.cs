namespace SeahawkSaverFrontend.Application.Features.UseCases.User.Login.DTOs;
/**
 * <summary>
 * A data transfer object containing the data returned by the login user endpoint response.
 * </summary>
 */
public sealed record LoginUserEndpointResponse
{
	public required string Token { get; init; }
	public required LoginUserEndpointUserResponse User { get; init; }
}