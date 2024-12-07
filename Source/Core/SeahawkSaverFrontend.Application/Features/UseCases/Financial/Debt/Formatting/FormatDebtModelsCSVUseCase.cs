namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Debt.Formatting;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Abstractions.Formatting;
using SeahawkSaverFrontend.Application.Abstractions.UseCases.Financial.Formatting;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Debt.Formatting.DTOs;
using SeahawkSaverFrontend.Application.Utilities;
using SeahawkSaverFrontend.Domain.Models.Financial;
using SeahawkSaverFrontend.Domain.Models.Utilities;

/**
 * <summary>
 * A use case for formatting debts within a specified date range into a csv format.
 * </summary>
 */
public sealed class FormatDebtModelsCSVUseCase : FormatFinancialModelsCSVUseCase<DebtModel, DebtModelCSVRow>
{
	/**
	 * <summary>
	 * Instantiates a new <see cref="FormatDebtModelsCSVUseCase"/> instance.
	 * </summary>
	 * <param name="financialModelCache">The financial model cache to use.</param>
	 * <param name="csvFormatter">The csv formatter to use.</param>
	 */
	public FormatDebtModelsCSVUseCase(IFinancialModelCache<DebtModel> financialModelCache,
									  ICSVFormatter<DebtModel, DebtModelCSVRow> csvFormatter)
		: base(financialModelCache, csvFormatter)
	{

	}

	protected override bool IsModelWithinDateRange(DebtModel financialModel, DateRangeModel dateRangeModel)
	{
		return DateRangeUtilities.IsDateWithinDateRangeInclusive(financialModel.DateTime!.Value, dateRangeModel);
	}
}