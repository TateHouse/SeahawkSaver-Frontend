namespace SeahawkSaverFrontend.UI.Features.User.DTOs;
/**
 * <summary>
 * A data transfer object containing the logged-in user's properties.
 * </summary>
 */
public class UserModel
{
	public Guid UserId { get; set; }
	public string Email { get; set; } = string.Empty;
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public bool IsAdmin { get; set; }
}