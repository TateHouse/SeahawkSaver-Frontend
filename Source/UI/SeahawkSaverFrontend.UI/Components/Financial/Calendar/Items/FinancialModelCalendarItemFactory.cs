namespace SeahawkSaverFrontend.UI.Components.Financial.Calendar.Items;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Abstractions.UseCases;
using SeahawkSaverFrontend.Domain.Models.Financial;
using SeahawkSaverFrontend.UI.Components.Financial.Calendar.UseCases;

/**
 * <summary>
 * A factory for creating <see cref="FinancialModelCalendarItem"/> instances for a specific
 * <see cref="IFinancialModelCache{TFinancialModel}"/>.
 * </summary>
 * <typeparam name="TFinancialModel">The financial model.</typeparam>
 * <typeparam name="TUseCase">The use case.</typeparam>
 */
public sealed class FinancialModelCalendarItemFactory<TFinancialModel, TUseCase> : IFinancialModelCalendarItemFactory<TFinancialModel>
	where TFinancialModel : FinancialModel
	where TUseCase : CreateFinancialModelCalendarItemsUseCase<TFinancialModel>
{
	private readonly IUseCaseFactory useCaseFactory;
	private readonly IFinancialModelCache<TFinancialModel> financialModelCache;

	/**
	 * <summary>
	 * Instantiates a new <see cref="FinancialModelCalendarItemFactory{TFinancialModel,TUseCase}"/> instance.
	 * </summary>
	 * <param name="useCaseFactory">The use case factory to use.</param>
	 * <param name="financialModelCache">The financial model cache to use.</param>
	 */
	public FinancialModelCalendarItemFactory(IUseCaseFactory useCaseFactory,
											 IFinancialModelCache<TFinancialModel> financialModelCache)
	{
		this.useCaseFactory = useCaseFactory;
		this.financialModelCache = financialModelCache;
	}

	public IList<FinancialModelCalendarItem> Create()
	{
		var models = financialModelCache.List();
		var useCase = useCaseFactory.Create<TUseCase>();
		var calendarItems = useCase.ExecuteAsync(models);

		return calendarItems.Result;
	}
}