namespace SeahawkSaverFrontend.UI.Features.User.Login.DTOs;
using System.ComponentModel.DataAnnotations;

/**
 * <summary>
 * A data transfer object containing the user's provided log in credentials.
 * </summary>
 */
public sealed class UserCredentialsModel
{
	[Required]
	[EmailAddress]
	public string Email { get; set; }

	[Required]
	public string Password { get; set; }
}