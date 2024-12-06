namespace SeahawkSaverFrontend.UI.Components.Financial.Calendar.UseCases;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Domain.Models.Financial;
using SeahawkSaverFrontend.UI.Components.Financial.Calendar.Items;

/**
 * <summary>
 * A use case for creating <see cref="SubscriptionModelCalendarItem"/> instances.
 * </summary>
 */
public sealed class CreateSubscriptionModelCalendarItemsUseCase : CreateFinancialModelCalendarItemsUseCase<SubscriptionModel>
{
	/**
	 * <summary>
	 * Instantiates a new <see cref="CreateSubscriptionModelCalendarItemsUseCase"/> instance.
	 * </summary>
	 * <param name="financialModelCache">The financial model cache to use.</param>
	 */
	public CreateSubscriptionModelCalendarItemsUseCase(IFinancialModelCache<SubscriptionModel> financialModelCache)
		: base(financialModelCache)
	{

	}

	protected override FinancialModelCalendarItem CreateCalendarItem(FinancialModel financialModel)
	{
		var subscriptionModel = (SubscriptionModel)financialModel;

		return new SubscriptionModelCalendarItem
		{
			FinancialModel = subscriptionModel,
			Start = subscriptionModel.DateTime!.Value,
			End = null,
			AllDay = true,
			Text = $"${subscriptionModel.Amount}"
		};
	}
}