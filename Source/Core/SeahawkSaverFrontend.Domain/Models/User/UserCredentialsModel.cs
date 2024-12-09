namespace SeahawkSaverFrontend.Domain.Models.User;
using System.ComponentModel.DataAnnotations;

public class UserCredentialsModel
{
	[Required]
	[EmailAddress]
	public string Email { get; set; } = string.Empty;

	[Required]
	public string Password { get; set; } = string.Empty;
}