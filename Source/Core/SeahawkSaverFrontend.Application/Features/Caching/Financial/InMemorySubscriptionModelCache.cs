namespace SeahawkSaverFrontend.Application.Features.Caching.Financial;
using SeahawkSaverFrontend.Application.Abstractions.UseCases;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Subscription.List;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * An in-memory data cache containing the subscription models.
 * </summary>
 */
public sealed class InMemorySubscriptionModelCache : InMemoryFinancialModelCache<SubscriptionModel>
{
	private readonly IUseCaseFactory useCaseFactory;

	/**
	 * <summary>
	 * Instantiates a new <see cref="InMemorySubscriptionModelCache"/> instance.
	 * </summary>
	 * <param name="useCaseFactory">The use case factory to use.</param>
	 */
	public InMemorySubscriptionModelCache(IUseCaseFactory useCaseFactory)
	{
		this.useCaseFactory = useCaseFactory;

	}

	protected override void MapUpdatedFinancialModel(SubscriptionModel cached, SubscriptionModel updated)
	{
		cached.Amount = updated.Amount;
		cached.DateTime = updated.DateTime;
	}

	public override async Task LoadAsync()
	{
		var useCase = useCaseFactory.Create<ListSubscriptionModelUseCase>();
		models = (await useCase.ExecuteAsync(null)).ToList();
	}
}