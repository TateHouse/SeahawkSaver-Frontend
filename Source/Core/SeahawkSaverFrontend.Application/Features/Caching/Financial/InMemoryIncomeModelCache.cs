namespace SeahawkSaverFrontend.Application.Features.Caching.Financial;
using SeahawkSaverFrontend.Application.Abstractions.UseCases;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Income.List;
using SeahawkSaverFrontend.Application.Utilities;
using SeahawkSaverFrontend.Domain.Models.Financial;
using SeahawkSaverFrontend.Domain.Models.Utilities;

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
	 */
	public InMemoryIncomeModelCache(IUseCaseFactory useCaseFactory)
	{
		this.useCaseFactory = useCaseFactory;
	}

	protected override void MapUpdatedFinancialModel(IncomeModel cached, IncomeModel updated)
	{
		cached.Amount = updated.Amount;
		cached.DateTime = updated.DateTime;
	}

	protected override decimal CalculateTotal()
	{
		return models.Aggregate(0.0m, (accumulator, income) => accumulator + income.Amount);
	}

	protected override decimal CalculateTotal(DateRangeModel dateRangeModel, out int modelCount)
	{
		var total = 0.0m;
		var count = 0;

		foreach (var income in models)
		{
			var isWithinDateRange = DateRangeUtilities.IsDateWithinDateRangeInclusive(income.DateTime!.Value, dateRangeModel);

			if (!isWithinDateRange)
			{
				continue;
			}

			total += income.Amount;
			++count;
		}

		modelCount = count;
		return total;
	}

	public override async Task LoadAsync()
	{
		var useCase = useCaseFactory.Create<ListIncomeModelUseCase>();
		models = (await useCase.ExecuteAsync(null)).ToList();
	}
}