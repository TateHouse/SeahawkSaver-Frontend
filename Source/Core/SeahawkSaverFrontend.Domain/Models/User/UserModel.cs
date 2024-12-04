namespace SeahawkSaverFrontend.Domain.Models.User;
/**
 * <summary>
 * A model containing the user data.
 * </summary>
 */
public class UserModel
{
	public required Guid UserId { get; set; }
	public required string Email { get; set; } = string.Empty;
	public required string FirstName { get; set; } = string.Empty;
	public required string LastName { get; set; } = string.Empty;
	public required bool IsAdmin { get; set; }
	public required bool IsActive { get; set; }
}