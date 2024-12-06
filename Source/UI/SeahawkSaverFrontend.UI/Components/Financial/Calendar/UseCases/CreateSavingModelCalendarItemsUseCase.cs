namespace SeahawkSaverFrontend.UI.Components.Financial.Calendar.UseCases;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Domain.Models.Financial;
using SeahawkSaverFrontend.UI.Components.Financial.Calendar.Items;

/**
 * <summary>
 * A use case for creating <see cref="SavingModelCalendarItem"/> instances.
 * </summary>
 */
public sealed class CreateSavingModelCalendarItemsUseCase : CreateFinancialModelCalendarItemsUseCase<SavingModel>
{
	/**
	 * <summary>
	 * Instantiates a new <see cref="CreateSavingModelCalendarItemsUseCase"/> instance.
	 * </summary>
	 * <param name="financialModelCache">The financial model cache to use.</param>
	 */
	public CreateSavingModelCalendarItemsUseCase(IFinancialModelCache<SavingModel> financialModelCache)
		: base(financialModelCache)
	{

	}

	protected override FinancialModelCalendarItem CreateCalendarItem(FinancialModel financialModel)
	{
		var savingModel = (SavingModel)financialModel;

		return new SavingModelCalendarItem
		{
			FinancialModel = savingModel,
			Start = savingModel.DateTime!.Value,
			End = null,
			AllDay = true,
			Text = $"Saving: ${savingModel.Amount}"
		};
	}
}