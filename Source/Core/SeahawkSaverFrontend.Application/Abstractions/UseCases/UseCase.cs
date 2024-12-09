namespace SeahawkSaverFrontend.Application.Abstractions.UseCases;
/**
 * <summary>
 * An abstract base class for all use cases to derive from.
 * </summary>
 * <typeparam name="TInput">The type of the input provided to the use case.</typeparam>
 * <typeparam name="TOutput">The type of the output provided from the use case.</typeparam>
 */
public abstract class UseCase<TInput, TOutput> : IUseCase<TInput, TOutput>, IUseCase
{
	public Type InputType { get; } = typeof(TInput);
	public Type OutputType { get; } = typeof(TOutput);

	public abstract Task<TOutput> ExecuteAsync(TInput input);
}