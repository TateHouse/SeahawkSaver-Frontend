namespace SeahawkSaverFrontend.Application.Features.Caching.Financial;
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
	 */
	public InMemoryDebtModelCache(IUseCaseFactory useCaseFactory)
	{
		this.useCaseFactory = useCaseFactory;
	}

	protected override void MapUpdatedFinancialModel(DebtModel cached, DebtModel updated)
	{
		cached.Amount = updated.Amount;
		cached.DateTime = updated.DateTime;
	}

	public override async Task LoadAsync()
	{
		var useCase = useCaseFactory.Create<ListDebtModelUseCase>();
		models = (await useCase.ExecuteAsync(null)).ToList();
	}
}