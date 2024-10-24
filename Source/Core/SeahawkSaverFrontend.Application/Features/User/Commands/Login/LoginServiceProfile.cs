namespace SeahawkSaverFrontend.Application.Features.User.Commands.Login;
using AutoMapper;
using SeahawkSaverFrontend.Application.Features.User.Commands.Login.DTOs;
using SeahawkSaverFrontend.Domain.Models;

public sealed class LoginServiceProfile : Profile
{
	public LoginServiceProfile()
	{
		CreateMap<string, Token>()
			.ForMember(destinationMember => destinationMember.Value,
					   memberOptions => memberOptions.MapFrom(sourceMember => sourceMember));

		CreateMap<LoginUserEndpointUserResponse, User>();
	}
}