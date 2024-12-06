namespace SeahawkSaverFrontend.Application.Features.Caching.Financial;
using AutoMapper;
using SeahawkSaverFrontend.Application.Abstractions.UseCases;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Saving.List;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * An in-memory data cache containing the saving models.
 * </summary>
 */
public sealed class InMemorySavingModelCache : InMemoryFinancialModelCache<SavingModel>
{
	private readonly IUseCaseFactory useCaseFactory;

	/**
	 * <summary>
	 * Instantiates a new <see cref="InMemorySavingModelCache"/> instance.
	 * </summary>
	 * <param name="mapper">The mapper to use.</param>
	 * <param name="useCaseFactory">The use case factory to use.</param>
	 */
	public InMemorySavingModelCache(IMapper mapper, IUseCaseFactory useCaseFactory)
		: base(mapper)
	{
		this.useCaseFactory = useCaseFactory;
	}

	public override async Task LoadAsync()
	{
		var useCase = useCaseFactory.Create<ListSavingModelUseCase>();
		models = (await useCase.ExecuteAsync(null)).ToList();
	}
}