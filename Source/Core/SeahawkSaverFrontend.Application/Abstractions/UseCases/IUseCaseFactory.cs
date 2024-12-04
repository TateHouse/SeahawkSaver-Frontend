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
	 * <param name="useCaseType">The type of the use case.</param>
	 * <returns>A <see cref="IUseCase{TInput,TOutput}"/> of the specified type.</returns>
	 */
	public IUseCase<TInput, TOutput> Create<TInput, TOutput>(Type useCaseType);
}