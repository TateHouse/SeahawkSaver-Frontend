namespace SeahawkSaverFrontend.Application.Features.Caching.Financial;
using SeahawkSaverFrontend.Application.Abstractions.UseCases;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Debt.List;
using SeahawkSaverFrontend.Application.Utilities;
using SeahawkSaverFrontend.Domain.Models.Financial;
using SeahawkSaverFrontend.Domain.Models.Utilities;

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

	protected override decimal CalculateTotal()
	{
		return models.Aggregate(0.0m, (accumulator, debt) => accumulator + debt.Amount);
	}

	protected override decimal CalculateTotal(DateRangeModel dateRangeModel,
											  out int modelCount,
											  out decimal smallestAmount,
											  out decimal largestAmount)
	{
		var total = 0.0m;
		var count = 0;
		smallestAmount = decimal.MaxValue;
		largestAmount = decimal.MinValue;

		foreach (var debt in models)
		{
			var isWithinDateRange = DateRangeUtilities.IsDateWithinDateRangeInclusive(debt.DateTime!.Value, dateRangeModel);

			if (!isWithinDateRange)
			{
				continue;
			}

			total += debt.Amount;
			++count;
			MathUtilities.UpdateMinMax(debt.Amount, ref smallestAmount, ref largestAmount);
		}

		if (count == 0)
		{
			smallestAmount = 0.0m;
			largestAmount = 0.0m;
		}

		modelCount = count;

		return total;
	}

	public override async Task LoadAsync()
	{
		var useCase = useCaseFactory.Create<ListDebtModelUseCase>();
		models = (await useCase.ExecuteAsync(null)).ToList();
	}
}