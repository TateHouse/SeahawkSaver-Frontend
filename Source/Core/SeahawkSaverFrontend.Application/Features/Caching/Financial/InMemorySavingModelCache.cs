namespace SeahawkSaverFrontend.Application.Features.Caching.Financial;
using SeahawkSaverFrontend.Application.Abstractions.UseCases;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Saving.List;
using SeahawkSaverFrontend.Application.Utilities;
using SeahawkSaverFrontend.Domain.Models.Financial;
using SeahawkSaverFrontend.Domain.Models.Utilities;

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
	 * <param name="useCaseFactory">The use case factory to use.</param>
	 */
	public InMemorySavingModelCache(IUseCaseFactory useCaseFactory)
	{
		this.useCaseFactory = useCaseFactory;
	}

	protected override void MapUpdatedFinancialModel(SavingModel cached, SavingModel updated)
	{
		cached.Amount = updated.Amount;
		cached.DateTime = updated.DateTime;
	}

	protected override decimal CalculateTotal()
	{
		return models.Aggregate(0.0m, (accumulator, saving) => accumulator + saving.Amount);
	}

	protected override decimal CalculateTotal(DateRangeModel dateRangeModel, out int modelCount)
	{
		var total = 0.0m;
		var count = 0;

		foreach (var saving in models)
		{
			var isWithinDateRange = DateRangeUtilities.IsDateWithinDateRangeInclusive(saving.DateTime!.Value, dateRangeModel);

			if (!isWithinDateRange)
			{
				continue;
			}

			total += saving.Amount;
			++count;
		}

		modelCount = count;

		return total;
	}

	public override async Task LoadAsync()
	{
		var useCase = useCaseFactory.Create<ListSavingModelUseCase>();
		models = (await useCase.ExecuteAsync(null)).ToList();
	}
}