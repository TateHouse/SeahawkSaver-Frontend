namespace SeahawkSaverFrontend.Application.Features.UseCases.User.Update.DTOs;
/**
 * <summary>
 * A data transfer object containing the data provided in the update user endpoint request.
 * </summary>
 */
public sealed record UpdateUserEndpointRequest
{
	public required UpdateUserEndpointUserRequest User { get; init; }
}