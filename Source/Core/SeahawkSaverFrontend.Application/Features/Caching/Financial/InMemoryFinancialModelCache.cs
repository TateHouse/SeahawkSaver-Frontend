namespace SeahawkSaverFrontend.Application.Features.Caching.Financial;
using AutoMapper;
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
	private readonly IMapper mapper;
	protected IList<TFinancialModel> models = new List<TFinancialModel>();

	/**
	 * <summary>
	 * Instantiates a new <see cref="InMemoryFinancialModelCache{TFinancialModel}"/> instance.
	 * </summary>
	 * <param name="mapper">The mapper to use.</param>
	 */
	protected InMemoryFinancialModelCache(IMapper mapper)
	{
		this.mapper = mapper;
	}

	public IEnumerable<TFinancialModel> List()
	{
		return models;
	}

	public void Add(TFinancialModel financialModel)
	{
		models.Add(financialModel);
	}

	public void Update(TFinancialModel financialModel)
	{
		var model = models.SingleOrDefault(model => model.Id == financialModel.Id);

		if (model == null)
		{
			throw new InvalidOperationException($"A financial model does not exist with the id: {financialModel.Id}");
		}

		mapper.Map(financialModel, model);
	}

	public void Delete(TFinancialModel financialModel)
	{
		models.Remove(financialModel);
	}

	public abstract Task LoadAsync();
}