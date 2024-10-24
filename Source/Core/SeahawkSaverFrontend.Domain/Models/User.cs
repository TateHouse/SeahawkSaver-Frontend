namespace SeahawkSaverFrontend.Domain.Models;
public sealed class User
{
	public required Guid UserId { get; init; }
	public required string Email { get; init; }
	public required string FirstName { get; init; }
	public required string LastName { get; init; }
}