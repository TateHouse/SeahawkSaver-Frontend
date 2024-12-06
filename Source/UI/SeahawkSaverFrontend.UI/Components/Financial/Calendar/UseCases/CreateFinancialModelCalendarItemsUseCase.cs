namespace SeahawkSaverFrontend.UI.Components.Financial.Calendar.UseCases;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Abstractions.UseCases;
using SeahawkSaverFrontend.Domain.Models.Financial;
using SeahawkSaverFrontend.UI.Components.Financial.Calendar.Items;

/**
 * <summary>
 * A use case for creating <see cref="FinancialModelCalendarItem"/> instances.
 * </summary>
 * <typeparam name="TFinancialModel">The type of financial model.</typeparam>
 */
public abstract class CreateFinancialModelCalendarItemsUseCase<TFinancialModel> : UseCase<IEnumerable<FinancialModel>, List<FinancialModelCalendarItem>>
	where TFinancialModel : FinancialModel
{
	private readonly IFinancialModelCache<TFinancialModel> financialModelCache;

	/**
	 * <summary>
	 * Instantiates a new <see cref="CreateFinancialModelCalendarItemsUseCase{TFinancialModel}"/> instance.
	 * </summary>
	 * <param name="financialModelCache">The financial model cache to use.</param>
	 */
	protected CreateFinancialModelCalendarItemsUseCase(IFinancialModelCache<TFinancialModel> financialModelCache)
	{
		this.financialModelCache = financialModelCache;
	}

	/**
	 * <summary>
	 * Creates the <see cref="FinancialModelCalendarItem"/>.
	 * </summary>
	 * <param name="financialModel">The financial model to create the <see cref="FinancialModelCalendarItem"/> for.</param>
	 * <returns>A <see cref="FinancialModelCalendarItem"/> for the given <paramref name="financialModel"/>.</returns>
	 */
	protected abstract FinancialModelCalendarItem CreateCalendarItem(FinancialModel financialModel);

	public override async Task<List<FinancialModelCalendarItem>> ExecuteAsync(IEnumerable<FinancialModel> input)
	{
		var models = financialModelCache.List();
		var calendarItems = models.Select(CreateCalendarItem).ToList();

		return await Task.FromResult(calendarItems);
	}
}