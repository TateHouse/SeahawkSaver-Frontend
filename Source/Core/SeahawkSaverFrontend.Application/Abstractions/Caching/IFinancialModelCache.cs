namespace SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Domain.Models.Financial;
using SeahawkSaverFrontend.Domain.Models.Utilities;

/**
 * <summary>
 * An interface for a data cache containing <typeparamref name="TFinancialModel"/> related data.
 * </summary>
 * <typeparam name="TFinancialModel">The type of financial model stored in the cache.</typeparam>
 */
public interface IFinancialModelCache<TFinancialModel>
	where TFinancialModel : FinancialModel
{
	/**
	 * <summary>
	 * Retrieves all data from the cache.
	 * </summary>
	 * <returns>An enumerable of <typeparamref name="TFinancialModel"/>.</returns>
	 */
	public IEnumerable<TFinancialModel> List();

	/**
	 * <summary>
	 * Adds the specified financial model to the cache.
	 * </summary>
	 * <param name="financialModel">The model to add.</param>
	 */
	public void Add(TFinancialModel financialModel);

	/**
	 * <summary>
	 * Updates the specified financial model in the cache.
	 * </summary>
	 * <param name="financialModel">The model to update.</param>
	 */
	public void Update(TFinancialModel financialModel);

	/**
	 * <summary>
	 * Deletes the specified financial model from the cache.
	 * </summary>
	 * <param name="financialModel">The model to delete.</param>
	 */
	public void Delete(TFinancialModel financialModel);

	/**
	 * <summary>
	 * Calculates the total of all <see cref="TFinancialModel"/> in the cache within the specified date range.
	 * </summary>
	 * <param name="dateRangeModel">An optional date range. If null, all models in the cache will be used.</param>
	 * <param name="modelCount">The number of models used in the calculation.</param>
	 * <param name="smallestAmount">The smallest amount in a model.</param>
	 * <param name="largestAmount">The largest amount in a model.</param>
	 */
	public decimal GetTotal(DateRangeModel? dateRangeModel,
							out int modelCount,
							out decimal smallestAmount,
							out decimal largestAmount);

	/**
	 * <summary>
	 * Loads the financial model data from the backend API into the cache.
	 * </summary>
	 */
	public Task LoadAsync();
}