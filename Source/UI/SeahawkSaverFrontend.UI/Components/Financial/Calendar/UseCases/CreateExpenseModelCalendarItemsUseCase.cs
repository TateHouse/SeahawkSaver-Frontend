namespace SeahawkSaverFrontend.UI.Components.Financial.Calendar.UseCases;
using MudBlazor;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Domain.Models.Financial;
using SeahawkSaverFrontend.UI.Components.Financial.Calendar.Items;

/**
 * <summary>
 * A use case for creating <see cref="ExpenseModelCalendarItem"/> instances.
 * </summary>
 */
public sealed class CreateExpenseModelCalendarItemsUseCase : CreateFinancialModelCalendarItemsUseCase<ExpenseModel>
{
	/**
	 * <summary>
	 * Instantiates a new <see cref="CreateExpenseModelCalendarItemsUseCase"/> instance.
	 * </summary>
	 * <param name="financialModelCache">The financial model cache to use.</param>
	 */
	public CreateExpenseModelCalendarItemsUseCase(IFinancialModelCache<ExpenseModel> financialModelCache)
		: base(financialModelCache)
	{

	}

	protected override FinancialModelCalendarItem CreateCalendarItem(FinancialModel financialModel)
	{
		var expenseModel = (ExpenseModel)financialModel;

		return new ExpenseModelCalendarItem
		{
			FinancialModel = expenseModel,
			HexColor = Colors.Orange.Default,
			Start = expenseModel.DateTime!.Value,
			End = null,
			AllDay = true,
			Text = $"${expenseModel.Amount}"
		};
	}
}