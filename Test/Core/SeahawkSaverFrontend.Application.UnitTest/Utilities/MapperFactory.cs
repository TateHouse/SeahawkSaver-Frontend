namespace SeahawkSaverFrontend.Application.UnitTest.Utilities;
using AutoMapper;

public class MapperFactory
{
	public static IMapper Create<TProfile>()
		where TProfile : Profile, new()
	{
		var configuration = new MapperConfiguration(configure =>
		{
			configure.AddProfile<TProfile>();
		});

		return configuration.CreateMapper();
	}
}