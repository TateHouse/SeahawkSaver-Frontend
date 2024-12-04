namespace SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Domain.Models.Financial;

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
}