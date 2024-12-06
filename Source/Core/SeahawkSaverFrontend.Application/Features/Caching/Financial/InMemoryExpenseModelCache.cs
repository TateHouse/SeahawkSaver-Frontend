namespace SeahawkSaverFrontend.Application.Features.Caching.Financial;
using SeahawkSaverFrontend.Application.Abstractions.UseCases;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Expense.List;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * An in-memory data cache containing the expense models.
 * </summary>
 */
public sealed class InMemoryExpenseModelCache : InMemoryFinancialModelCache<ExpenseModel>
{
	private readonly IUseCaseFactory useCaseFactory;

	/**
	 * <summary>
	 * Instantiates a new <see cref="InMemoryExpenseModelCache"/> instance.
	 * </summary>
	 * <param name="useCaseFactory">The use case factory to use.</param>
	 */
	public InMemoryExpenseModelCache(IUseCaseFactory useCaseFactory)
	{
		this.useCaseFactory = useCaseFactory;
	}

	protected override void MapUpdatedFinancialModel(ExpenseModel cached, ExpenseModel updated)
	{
		cached.Amount = updated.Amount;
		cached.DateTime = updated.DateTime;
	}

	public override async Task LoadAsync()
	{
		var useCase = useCaseFactory.Create<ListExpenseModelUseCase>();
		models = (await useCase.ExecuteAsync(null)).ToList();
	}
}