namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Saving;
using Microsoft.Extensions.DependencyInjection;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Saving.Create;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Saving.Delete;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Saving.Formatting;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Saving.List;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Saving.Update;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A class for registering the <see cref="SavingModel"/> related use cases.
 * </summary>
 */
internal static class UseCaseRegistration
{
	/**
	 * <summary>
	 * An extension method for <see cref="IServiceCollection"/> to register the <see cref="SavingModel"/> related use
	 * cases.
	 * </summary>
	 */
	internal static void RegisterSavingUseCases(this IServiceCollection services)
	{
		services.AddTransient<CreateSavingModelUseCase>();
		services.AddTransient<DeleteSavingModelUseCase>();
		services.AddTransient<FormatSavingModelsCSVUseCase>();
		services.AddTransient<ListSavingModelUseCase>();
		services.AddTransient<UpdateSavingModelUseCase>();
	}
}