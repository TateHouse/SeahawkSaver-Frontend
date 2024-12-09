namespace SeahawkSaverFrontend.Application.Features.UseCases;
using Microsoft.Extensions.DependencyInjection;
using SeahawkSaverFrontend.Application.Abstractions.UseCases;

/**
 * <summary>
 * A factory to instantiate use cases.
 * </summary>
 */
public sealed class UseCaseFactory : IUseCaseFactory
{
	private readonly IServiceProvider serviceProvider;

	/**
	 * <summary>
	 * Instantiates a new <see cref="UseCaseFactory"/>.
	 * </summary>
	 * <param name="serviceProvider">The service provider to use.</param>
	 */
	public UseCaseFactory(IServiceProvider serviceProvider)
	{
		this.serviceProvider = serviceProvider;
	}

	public TUseCase Create<TUseCase>()
		where TUseCase : IUseCase
	{
		return (TUseCase)serviceProvider.GetRequiredService(typeof(TUseCase));
	}
}