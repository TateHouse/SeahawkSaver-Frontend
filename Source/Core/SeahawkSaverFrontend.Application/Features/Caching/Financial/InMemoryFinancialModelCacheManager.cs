namespace SeahawkSaverFrontend.Application.Features.Caching.Financial;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A class to manage all in-memory financial model caches.
 * </summary>
 */
public sealed class InMemoryFinancialModelCacheManager
{
	public IFinancialModelCache<DebtModel> DebtModelCache { get; init; }
	public IFinancialModelCache<ExpenseModel> ExpenseModelCache { get; init; }
	public IFinancialModelCache<IncomeModel> IncomeModelCache { get; init; }
	public IFinancialModelCache<SavingModel> SavingModelCache { get; init; }
	public IFinancialModelCache<SubscriptionModel> SubscriptionModelCache { get; init; }

	/**
	 * <summary>
	 * Instantiates a new <see cref="InMemoryFinancialModelCacheManager"/>.
	 * </summary>
	 * <param name="debtModelCache">The in-memory debt cache to use.</param>
	 * <param name="expenseModelCache">The in-memory expense cache to use.</param>
	 * <param name="incomeModelCache">The in-memory income cache to use.</param>
	 * <param name="savingModelCache">The in-memory saving cache to use.</param>
	 * <param name="subscriptionModelCache">The in-memory subscription cache to use.</param>
	 */
	public InMemoryFinancialModelCacheManager(IFinancialModelCache<DebtModel> debtModelCache,
											  IFinancialModelCache<ExpenseModel> expenseModelCache,
											  IFinancialModelCache<IncomeModel> incomeModelCache,
											  IFinancialModelCache<SavingModel> savingModelCache,
											  IFinancialModelCache<SubscriptionModel> subscriptionModelCache)
	{
		DebtModelCache = debtModelCache;
		ExpenseModelCache = expenseModelCache;
		IncomeModelCache = incomeModelCache;
		SavingModelCache = savingModelCache;
		SubscriptionModelCache = subscriptionModelCache;
	}

	public async Task LoadAsync()
	{
		await Task.WhenAll(DebtModelCache.LoadAsync(),
						   ExpenseModelCache.LoadAsync(),
						   IncomeModelCache.LoadAsync(),
						   SavingModelCache.LoadAsync(),
						   SubscriptionModelCache.LoadAsync());
	}
}