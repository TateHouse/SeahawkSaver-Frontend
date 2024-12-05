namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Debt;
using Microsoft.Extensions.DependencyInjection;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Debt.Create;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Debt.Delete;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Debt.List;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Debt.Update;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A class for registering the <see cref="DebtModel"/> related use cases.
 * </summary>
 */
internal static class UseCaseRegistration
{
	/**
	 * <summary>
	 * An extension method for <see cref="IServiceCollection"/> to register the <see cref="DebtModel"/> related use
	 * cases.
	 * </summary>
	 */
	internal static void RegisterDebtUseCases(this IServiceCollection services)
	{
		services.AddTransient<CreateDebtModelUseCase>();
		services.AddTransient<DeleteDebtModelUseCase>();
		services.AddTransient<ListDebtModelUseCase>();
		services.AddTransient<UpdateDebtModelUseCase>();
	}
}