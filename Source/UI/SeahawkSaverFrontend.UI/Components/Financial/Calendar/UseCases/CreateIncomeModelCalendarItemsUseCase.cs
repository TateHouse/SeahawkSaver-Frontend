namespace SeahawkSaverFrontend.UI.Components.Financial.Calendar.UseCases;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Domain.Models.Financial;
using SeahawkSaverFrontend.UI.Components.Financial.Calendar.Items;

/**
 * <summary>
 * A use case for creating <see cref="IncomeModelCalendarItem"/> instances.
 * </summary>
 */
public sealed class CreateIncomeModelCalendarItemsUseCase : CreateFinancialModelCalendarItemsUseCase<IncomeModel>
{
	/**
	 * <summary>
	 * Instantiates a new <see cref="CreateIncomeModelCalendarItemsUseCase"/> instance.
	 * </summary>
	 * <param name="financialModelCache">The financial model cache to use.</param>
	 */
	public CreateIncomeModelCalendarItemsUseCase(IFinancialModelCache<IncomeModel> financialModelCache)
		: base(financialModelCache)
	{

	}

	protected override FinancialModelCalendarItem CreateCalendarItem(FinancialModel financialModel)
	{
		var incomeModel = (IncomeModel)financialModel;

		return new IncomeModelCalendarItem
		{
			FinancialModel = incomeModel,
			Start = incomeModel.DateTime!.Value,
			End = null,
			AllDay = true,
			Text = $"Income: ${incomeModel.Amount}"
		};
	}
}