namespace SeahawkSaverFrontend.Application.Abstractions.UseCases.Financial.Formatting;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Abstractions.Formatting;
using SeahawkSaverFrontend.Application.Abstractions.UseCases.Financial.Formatting.DTOs;
using SeahawkSaverFrontend.Domain.Models.Financial;
using SeahawkSaverFrontend.Domain.Models.Utilities;

/**
 * <summary>
 * An abstract base class for all financial models to format the models within a given date range into a csv format.
 * </summary>
 * <typeparam name="TFinancialModel">The type of the financial model.</typeparam>
 * <typeparam name="TFinancialModelCSVRow">The type of the financial model csv row.</typeparam>
 */
public abstract class FormatFinancialModelsCSVUseCase<TFinancialModel, TFinancialModelCSVRow> : UseCase<DateRangeModel, IReadOnlyList<TFinancialModelCSVRow>>
	where TFinancialModel : FinancialModel
	where TFinancialModelCSVRow : FinancialModelCSVRow
{
	private readonly IFinancialModelCache<TFinancialModel> financialModelCache;
	private readonly ICSVFormatter<TFinancialModel, TFinancialModelCSVRow> csvFormatter;

	/**
	 * <summary>
	 * Instantiates a new <see cref="FormatFinancialModelsCSVUseCase{TFinancialModel,TFinancialModelCSVRow}"/> instance.
	 * </summary>
	 * <param name="financialModelCache">The financial model cache to use.</param>
	 * <param name="csvFormatter">The csv formatter to use.</param>
	 */
	protected FormatFinancialModelsCSVUseCase(IFinancialModelCache<TFinancialModel> financialModelCache,
											  ICSVFormatter<TFinancialModel, TFinancialModelCSVRow> csvFormatter)
	{
		this.financialModelCache = financialModelCache;
		this.csvFormatter = csvFormatter;
	}

	/**
	 * <summary>
	 * Checks if the model is within the specified date range.
	 * </summary>
	 * <param name="financialModel">The model to check.</param>
	 * <param name="dateRangeModel">The date range to check against.</param>
	 */
	protected abstract bool IsModelWithinDateRange(TFinancialModel financialModel, DateRangeModel dateRangeModel);

	public override Task<IReadOnlyList<TFinancialModelCSVRow>> ExecuteAsync(DateRangeModel input)
	{
		var models = financialModelCache.List().ToList();
		var modelsWithinDateRange = new List<TFinancialModel>(models.Capacity);

		foreach (var model in models)
		{
			var isModelWithinDateRange = IsModelWithinDateRange(model, input);

			if (!isModelWithinDateRange)
			{
				continue;
			}

			modelsWithinDateRange.Add(model);
		}

		return Task.FromResult(csvFormatter.Format(modelsWithinDateRange));
	}
}