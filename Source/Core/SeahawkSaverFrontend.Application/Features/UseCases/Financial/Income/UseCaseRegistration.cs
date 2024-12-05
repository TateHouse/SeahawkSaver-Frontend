namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Income;
using Microsoft.Extensions.DependencyInjection;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Income.Create;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Income.Delete;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Income.List;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Income.Update;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A class for registering the <see cref="IncomeModel"/> related use cases.
 * </summary>
 */
internal static class UseCaseRegistration
{
	/**
	 * <summary>
	 * An extension method for <see cref="IServiceCollection"/> to register the <see cref="IncomeModel"/> related use
	 * cases.
	 * </summary>
	 */
	internal static void RegisterIncomeUseCases(this IServiceCollection services)
	{
		services.AddTransient<CreateIncomeModelUseCase>();
		services.AddTransient<DeleteIncomeModelUseCase>();
		services.AddTransient<ListIncomeModelUseCase>();
		services.AddTransient<UpdateIncomeModelUseCase>();
	}
}