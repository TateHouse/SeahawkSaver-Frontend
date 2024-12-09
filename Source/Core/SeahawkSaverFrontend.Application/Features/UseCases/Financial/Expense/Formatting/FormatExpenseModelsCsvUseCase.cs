namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Expense.Formatting;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Abstractions.Formatting;
using SeahawkSaverFrontend.Application.Abstractions.UseCases.Financial.Formatting;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Expense.Formatting.DTOs;
using SeahawkSaverFrontend.Application.Utilities;
using SeahawkSaverFrontend.Domain.Models.Financial;
using SeahawkSaverFrontend.Domain.Models.Utilities;

/**
 * <summary>
 * A use case for formatting expenses within a specified date range into a csv format.
 * </summary>
 */
public sealed class FormatExpenseModelsCSVUseCase : FormatFinancialModelsCSVUseCase<ExpenseModel, ExpenseModelCSVRow>
{
	/**
	 * <summary>
	 * Instantiates a new <see cref="FormatExpenseModelsCSVUseCase"/> instance.
	 * </summary>
	 * <param name="financialModelCache">The financial model cache to use.</param>
	 * <param name="csvFormatter">The csv formatter to use.</param>
	 */
	public FormatExpenseModelsCSVUseCase(IFinancialModelCache<ExpenseModel> financialModelCache, ICSVFormatter<ExpenseModel, ExpenseModelCSVRow> csvFormatter)
		: base(financialModelCache, csvFormatter)
	{

	}

	protected override bool IsModelWithinDateRange(ExpenseModel financialModel, DateRangeModel dateRangeModel)
	{
		return DateRangeUtilities.IsDateWithinDateRangeInclusive(financialModel.DateTime!.Value, dateRangeModel);
	}
}