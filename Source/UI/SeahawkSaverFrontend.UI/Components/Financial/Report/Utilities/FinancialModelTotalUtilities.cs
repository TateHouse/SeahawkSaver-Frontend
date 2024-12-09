namespace SeahawkSaverFrontend.UI.Components.Financial.Report.Utilities;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Domain.Models.Financial;
using SeahawkSaverFrontend.Domain.Models.Utilities;
using SeahawkSaverFrontend.UI.Components.Financial.Report.DTOs;

/**
 * <summary>
 * A collection of utility methods for the <see cref="FinancialModelTotal"/>.
 * </summary>
 */
public static class FinancialModelTotalUtilities
{
	/**
	 * <summary>
	 * Gets the total for a specific <see cref="FinancialModelType"/> within the specified date range.
	 * </summary>
	 * <param name="dateRangeModel">An optional date range. If null, then all models in the cache will be used.</param>
	 * <param name="financialModelCache">The model cache to use.</param>
	 * <param name="financialModelType">The type of the financial model.</param>
	 */
	public static FinancialModelTotal GetTotal<TFinancialModel>(DateRangeModel? dateRangeModel,
																IFinancialModelCache<TFinancialModel> financialModelCache,
																FinancialModelType financialModelType)
		where TFinancialModel : FinancialModel
	{
		return new FinancialModelTotal
		{
			Amount = financialModelCache.GetTotal(dateRangeModel,
												  out var count,
												  out var smallestAmount,
												  out var largestAmount),
			SmallestAmount = smallestAmount,
			LargestAmount = largestAmount,
			Count = count,
			Type = financialModelType
		};
	}
}