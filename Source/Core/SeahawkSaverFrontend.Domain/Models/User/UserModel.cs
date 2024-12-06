namespace SeahawkSaverFrontend.Domain.Models.User;
/**
 * <summary>
 * A model containing the user data.
 * </summary>
 */
public class UserModel
{
	public Guid UserId { get; set; }
	public string Email { get; set; } = string.Empty;
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public bool IsAdmin { get; set; }
	public bool IsActive { get; set; }
}