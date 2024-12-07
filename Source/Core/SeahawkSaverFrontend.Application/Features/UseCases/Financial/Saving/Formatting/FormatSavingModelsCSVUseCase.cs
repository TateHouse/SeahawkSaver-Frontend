namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Saving.Formatting;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Abstractions.Formatting;
using SeahawkSaverFrontend.Application.Abstractions.UseCases.Financial.Formatting;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Saving.Formatting.DTOs;
using SeahawkSaverFrontend.Application.Utilities;
using SeahawkSaverFrontend.Domain.Models.Financial;
using SeahawkSaverFrontend.Domain.Models.Utilities;

/**
 * <summary>
 * A use case for formatting savings within a specified date range into a csv format.
 * </summary>
 */
public sealed class FormatSavingModelsCSVUseCase : FormatFinancialModelsCSVUseCase<SavingModel, SavingModelCSVRow>
{
	/**
	 * <summary>
	 * Instantiates a new <see cref="FormatSavingModelsCSVUseCase"/> instance.
	 * </summary>
	 * <param name="financialModelCache">The financial model cache to use.</param>
	 * <param name="csvFormatter">The csv formatter to use.</param>
	 */
	public FormatSavingModelsCSVUseCase(IFinancialModelCache<SavingModel> financialModelCache, ICSVFormatter<SavingModel, SavingModelCSVRow> csvFormatter)
		: base(financialModelCache, csvFormatter)
	{

	}

	protected override bool IsModelWithinDateRange(SavingModel financialModel, DateRangeModel dateRangeModel)
	{
		return DateRangeUtilities.IsDateWithinDateRangeInclusive(financialModel.DateTime!.Value, dateRangeModel);
	}
}