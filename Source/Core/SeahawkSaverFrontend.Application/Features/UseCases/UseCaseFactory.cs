namespace SeahawkSaverFrontend.Application.Features.UseCases;
using Microsoft.Extensions.DependencyInjection;
using SeahawkSaverFrontend.Application.Abstractions.UseCases;

/**
 * <summary>
 * A factory for <see cref="IUseCase{TInput,TOutput}"/> instances.
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

	public IUseCase<TInput, TOutput> Create<TInput, TOutput>(Type useCaseType)
	{
		return (IUseCase<TInput, TOutput>)serviceProvider.GetRequiredService(useCaseType);
	}
}