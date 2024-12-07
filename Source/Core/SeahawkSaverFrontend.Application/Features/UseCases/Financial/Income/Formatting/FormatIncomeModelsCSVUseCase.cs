namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Income.Formatting;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Abstractions.Formatting;
using SeahawkSaverFrontend.Application.Abstractions.UseCases.Financial.Formatting;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Income.Formatting.DTOs;
using SeahawkSaverFrontend.Application.Utilities;
using SeahawkSaverFrontend.Domain.Models.Financial;
using SeahawkSaverFrontend.Domain.Models.Utilities;

/**
 * <summary>
 * A use case for formatting incomes within a specified date range into a csv format.
 * </summary>
 */
public sealed class FormatIncomeModelsCSVUseCase : FormatFinancialModelsCSVUseCase<IncomeModel, IncomeModelCSVRow>
{
	/**
	 * <summary>
	 * Instantiates a new <see cref="FormatIncomeModelsCSVUseCase"/> instance.
	 * </summary>
	 * <param name="financialModelCache">The financial model cache to use.</param>
	 * <param name="csvFormatter">The csv formatter to use.</param>
	 */
	public FormatIncomeModelsCSVUseCase(IFinancialModelCache<IncomeModel> financialModelCache, ICSVFormatter<IncomeModel, IncomeModelCSVRow> csvFormatter)
		: base(financialModelCache, csvFormatter)
	{

	}

	protected override bool IsModelWithinDateRange(IncomeModel financialModel, DateRangeModel dateRangeModel)
	{
		return DateRangeUtilities.IsDateWithinDateRangeInclusive(financialModel.DateTime!.Value, dateRangeModel);
	}
}