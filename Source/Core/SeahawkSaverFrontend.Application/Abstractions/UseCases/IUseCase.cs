namespace SeahawkSaverFrontend.Application.Abstractions.UseCases;
/**
 * <summary>
 * An interface for a use case.
 * </summary>
 * <typeparam name="TInput">The input provided to the use case.</typeparam>
 * <typeparam name="TOutput">The output provided from the use case.</typeparam>
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