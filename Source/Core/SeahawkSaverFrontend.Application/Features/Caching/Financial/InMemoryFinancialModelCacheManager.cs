namespace SeahawkSaverFrontend.Application.Features.Caching.Financial;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A class to manage all in-memory financial model caches.
 * </summary>
 */
public sealed class InMemoryFinancialModelCacheManager
{
	public InMemoryFinancialModelCache<DebtModel> DebtModelCache { get; init; }
	public InMemoryFinancialModelCache<ExpenseModel> ExpenseModelCache { get; init; }
	public InMemoryFinancialModelCache<IncomeModel> IncomeModelCache { get; init; }
	public InMemoryFinancialModelCache<SavingModel> SavingModelCache { get; init; }
	public InMemoryFinancialModelCache<SubscriptionModel> SubscriptionModelCache { get; init; }

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
	public InMemoryFinancialModelCacheManager(InMemoryFinancialModelCache<DebtModel> debtModelCache,
											  InMemoryFinancialModelCache<ExpenseModel> expenseModelCache,
											  InMemoryFinancialModelCache<IncomeModel> incomeModelCache,
											  InMemoryFinancialModelCache<SavingModel> savingModelCache,
											  InMemoryFinancialModelCache<SubscriptionModel> subscriptionModelCache)
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