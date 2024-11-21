namespace SeahawkSaverFrontend.UI.Features.User.Login.DTOs;
using System.Text.Json.Serialization;

/**
 * <summary>
 * A data transfer object containing the user properties returned from the login endpoint.
 * </summary>
 */
public class LoginUserEndpointUserResponse
{
	public required Guid UserId { get; init; }
	public required string Email { get; init; }
	public required string FirstName { get; init; }
	public required string LastName { get; init; }
	public required bool IsAdmin { get; init; }

	[JsonIgnore]
	public bool IsActive { get; init; } = true;
}