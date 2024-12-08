namespace SeahawkSaverFrontend.Application.Features.Caching.Financial;
using SeahawkSaverFrontend.Application.Abstractions.UseCases;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Expense.List;
using SeahawkSaverFrontend.Application.Utilities;
using SeahawkSaverFrontend.Domain.Models.Financial;
using SeahawkSaverFrontend.Domain.Models.Utilities;

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

	protected override decimal CalculateTotal()
	{
		return models.Aggregate(0.0m, (accumulator, expense) => accumulator + expense.Amount);
	}

	protected override decimal CalculateTotal(DateRangeModel dateRangeModel, out int modelCount)
	{
		var total = 0.0m;
		var count = 0;

		foreach (var expense in models)
		{
			var isWithinDateRange = DateRangeUtilities.IsDateWithinDateRangeInclusive(expense.DateTime!.Value, dateRangeModel);

			if (!isWithinDateRange)
			{
				continue;
			}

			total += expense.Amount;
			++count;
		}

		modelCount = count;
		return total;
	}

	public override async Task LoadAsync()
	{
		var useCase = useCaseFactory.Create<ListExpenseModelUseCase>();
		models = (await useCase.ExecuteAsync(null)).ToList();
	}
}