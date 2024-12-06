namespace SeahawkSaverFrontend.Application.Features.Caching.Financial;
using AutoMapper;
using SeahawkSaverFrontend.Application.Abstractions.UseCases;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Income.List;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * An in-memory data cache containing the income models.
 * </summary>
 */
public sealed class InMemoryIncomeModelCache : InMemoryFinancialModelCache<IncomeModel>
{
	private readonly IUseCaseFactory useCaseFactory;

	/**
	 * <summary>
	 * Instantiates a new <see cref="InMemoryIncomeModelCache"/> instance.
	 * </summary>
	 * <param name="useCaseFactory">The use case factory to use.</param>
	 * <param name="mapper">The mapper to use.</param>
	 */
	public InMemoryIncomeModelCache(IMapper mapper, IUseCaseFactory useCaseFactory)
		: base(mapper)
	{
		this.useCaseFactory = useCaseFactory;
	}

	public override async Task LoadAsync()
	{
		var useCase = useCaseFactory.Create<ListIncomeModelUseCase>();
		models = (await useCase.ExecuteAsync(null)).ToList();
	}
}