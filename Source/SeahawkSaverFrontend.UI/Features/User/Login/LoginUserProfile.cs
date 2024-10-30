namespace SeahawkSaverFrontend.UI.Features.User.Login;
using AutoMapper;
using SeahawkSaverFrontend.UI.Features.User.DTOs;
using SeahawkSaverFrontend.UI.Features.User.Login.DTOs;

/**
 * <summary>
 * The AutoMapper <see cref="Profile"/> for the <see cref="User"/> related mappings.
 * </summary>
 */
public sealed class LoginUserProfile : Profile
{
	/**
	 * <summary>
	 * Instantiates a new <see cref="LoginUserProfile"/> instance.
	 * </summary>
	 */
	public LoginUserProfile()
	{
		CreateMap<LoginUserEndpointUserResponse, UserModel>();
	}
}