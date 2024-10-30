namespace SeahawkSaverFrontend.UI.Features.User.Login.DTOs;
/**
 * <summary>
 * A data transfer object containing the data returned from the login endpoint.
 * </summary>
 */
public class LoginUserEndpointResponse
{
	public required string Token { get; init; }
	public required LoginUserEndpointUserResponse User { get; init; }
}