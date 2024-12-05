namespace SeahawkSaverFrontend.Application.Abstractions.UseCases;
/**
 * <summary>
 * An interface for a factory for <see cref="IUseCase{TInput,TOutput}"/> instances.
 * </summary>
 */
public interface IUseCaseFactory
{
	/**
	 * <summary>
	 * Instantiates a <see cref="IUseCase{TInput,TOutput}"/> of the specified type.
	 * </summary>
	 * <typeparam name="TUseCase">The type of the use case.</typeparam>
	 * <returns>A <see cref="IUseCase{TInput,TOutput}"/> of the specified type.</returns>
	 */
	public TUseCase Create<TUseCase>()
		where TUseCase : IUseCase;
}