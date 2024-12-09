namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Subscription;
using Microsoft.Extensions.DependencyInjection;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Subscription.Create;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Subscription.Delete;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Subscription.Formatting;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Subscription.List;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Subscription.Update;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A class for registering the <see cref="SubscriptionModel"/> related use cases.
 * </summary>
 */
internal static class UseCaseRegistration
{
	/**
	 * <summary>
	 * An extension method for <see cref="IServiceCollection"/> to register the <see cref="SubscriptionModel"/> related
	 * use cases.
	 * </summary>
	 */
	internal static void RegisterSubscriptionUseCases(this IServiceCollection services)
	{
		services.AddTransient<CreateSubscriptionModelUseCase>();
		services.AddTransient<DeleteSubscriptionModelUseCase>();
		services.AddTransient<FormatSubscriptionModelsCSVUseCase>();
		services.AddTransient<ListSubscriptionModelUseCase>();
		services.AddTransient<UpdateSubscriptionModelUseCase>();
	}
}