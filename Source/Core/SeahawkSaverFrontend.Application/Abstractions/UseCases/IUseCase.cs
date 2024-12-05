namespace SeahawkSaverFrontend.Application.Abstractions.UseCases;
/**
 * <summary>
 * A generic interface for a use case.
 * </summary>
 * <typeparam name="TInput">The type of the input provided to the use case.</typeparam>
 * <typeparam name="TOutput">The type of the output provided from the use case.</typeparam>
 */
public interface IUseCase<in TInput, TOutput>
{
	/**
	 * <summary>
	 * Asynchronously executes a use case.
	 * </summary>
	 * <param name="input">The input provided to the use case.</param>
	 * <returns>A task that represents the asynchronous operation, and it contains the <typeparamref name="TOutput"/> of
	 * the use case.</returns>
	 */
	public Task<TOutput> ExecuteAsync(TInput input);
}

/**
 * <summary>
 * A non-generic interface for a use case.
 * </summary>
 */
public interface IUseCase
{
	public Type InputType { get; }
	public Type OutputType { get; }
}