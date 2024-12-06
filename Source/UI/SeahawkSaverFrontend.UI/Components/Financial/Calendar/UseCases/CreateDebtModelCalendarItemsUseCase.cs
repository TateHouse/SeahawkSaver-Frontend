namespace SeahawkSaverFrontend.UI.Components.Financial.Calendar.UseCases;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Domain.Models.Financial;
using SeahawkSaverFrontend.UI.Components.Financial.Calendar.Items;

/**
 * <summary>
 * A use case for creating <see cref="DebtModelCalendarItem"/> instances.
 * </summary>
 */
public sealed class CreateDebtModelCalendarItemsUseCase : CreateFinancialModelCalendarItemsUseCase<DebtModel>
{
	/**
	 * <summary>
	 * Instantiates a new <see cref="CreateDebtModelCalendarItemsUseCase"/> instance.
	 * </summary>
	 * <param name="financialModelCache">The financial model cache to use.</param>
	 */
	public CreateDebtModelCalendarItemsUseCase(IFinancialModelCache<DebtModel> financialModelCache)
		: base(financialModelCache)
	{

	}

	protected override FinancialModelCalendarItem CreateCalendarItem(FinancialModel financialModel)
	{
		var debtModel = (DebtModel)financialModel;

		return new DebtModelCalendarItem
		{
			FinancialModel = debtModel,
			Start = debtModel.DateTime!.Value,
			End = null,
			AllDay = true,
			Text = $"Debt: ${debtModel.Amount}"
		};
	}
}