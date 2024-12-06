namespace SeahawkSaverFrontend.Application.Features.Caching.Financial;
using AutoMapper;
using SeahawkSaverFrontend.Application.Abstractions.UseCases;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Debt.List;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * An in-memory data cache containing the debt models.
 * </summary>
 */
public sealed class InMemoryDebtModelCache : InMemoryFinancialModelCache<DebtModel>
{
	private readonly IUseCaseFactory useCaseFactory;

	/**
	 * <summary>
	 * Instantiates a new <see cref="InMemoryDebtModelCache"/> instance.
	 * </summary>
	 * <param name="useCaseFactory">The use case factory to use.</param>
	 * <param name="mapper">The mapper to use.</param>
	 */
	public InMemoryDebtModelCache(IMapper mapper, IUseCaseFactory useCaseFactory)
		: base(mapper)
	{
		this.useCaseFactory = useCaseFactory;
	}

	public override async Task LoadAsync()
	{
		var useCase = useCaseFactory.Create<ListDebtModelUseCase>();
		models = (await useCase.ExecuteAsync(null)).ToList();
	}
}