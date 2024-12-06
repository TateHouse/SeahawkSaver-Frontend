namespace SeahawkSaverFrontend.Application.Features.Caching.Financial;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * An in-memory data cache containing <typeparamref name="TFinancialModel"/> related data.
 * </summary>
 * <typeparam name="TFinancialModel">The type of financial model stored in the cache.</typeparam>
 */
public abstract class InMemoryFinancialModelCache<TFinancialModel> : IFinancialModelCache<TFinancialModel>
	where TFinancialModel : FinancialModel
{
	protected IList<TFinancialModel> models = new List<TFinancialModel>();

	public IEnumerable<TFinancialModel> List()
	{
		return models;
	}

	public void Add(TFinancialModel financialModel)
	{
		models.Add(financialModel);
	}

	/**
	 * <summary>
	 * Updates the cached financial model's properties.
	 * </summary>
	 * <param name="cached">The model in the cache.</param>
	 * <param name="updated">The model with the updated properties.</param>
	 */
	protected abstract void MapUpdatedFinancialModel(TFinancialModel cached, TFinancialModel updated);

	public void Update(TFinancialModel financialModel)
	{
		var model = models.SingleOrDefault(model => model.Id == financialModel.Id);

		if (model == null)
		{
			throw new InvalidOperationException($"A financial model does not exist with the id: {financialModel.Id}");
		}

		MapUpdatedFinancialModel(model, financialModel);
	}

	public void Delete(TFinancialModel financialModel)
	{
		models.Remove(financialModel);
	}

	public abstract Task LoadAsync();
}