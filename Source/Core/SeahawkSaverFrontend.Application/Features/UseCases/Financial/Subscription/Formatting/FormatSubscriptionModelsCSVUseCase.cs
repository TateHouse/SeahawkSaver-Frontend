namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Subscription.Formatting;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Abstractions.Formatting;
using SeahawkSaverFrontend.Application.Abstractions.UseCases.Financial.Formatting;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Subscription.Formatting.DTOs;
using SeahawkSaverFrontend.Application.Utilities;
using SeahawkSaverFrontend.Domain.Models.Financial;
using SeahawkSaverFrontend.Domain.Models.Utilities;

/**
 * <summary>
 * A use case for formatting subscriptions within a specified date range into a csv format.
 * </summary>
 */
public sealed class FormatSubscriptionModelsCSVUseCase : FormatFinancialModelsCSVUseCase<SubscriptionModel, SubscriptionModelCSVRow>
{
	/**
	 * <summary>
	 * Instantiates a new <see cref="FormatSubscriptionModelsCSVUseCase"/> instance.
	 * </summary>
	 * <param name="financialModelCache">The financial model cache to use.</param>
	 * <param name="csvFormatter">The csv formatter to use.</param>
	 */
	public FormatSubscriptionModelsCSVUseCase(IFinancialModelCache<SubscriptionModel> financialModelCache, ICSVFormatter<SubscriptionModel, SubscriptionModelCSVRow> csvFormatter)
		: base(financialModelCache, csvFormatter)
	{

	}

	protected override bool IsModelWithinDateRange(SubscriptionModel financialModel, DateRangeModel dateRangeModel)
	{
		return DateRangeUtilities.IsDateWithinDateRangeInclusive(financialModel.DateTime!.Value, dateRangeModel);
	}
}