namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Expense;
using Microsoft.Extensions.DependencyInjection;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Expense.Create;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Expense.Delete;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Expense.Formatting;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Expense.List;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Expense.Update;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A class for registering the <see cref="ExpenseModel"/> related use cases.
 * </summary>
 */
internal static class UseCaseRegistration
{
	/**
	 * <summary>
	 * An extension method for <see cref="IServiceCollection"/> to register the <see cref="ExpenseModel"/> related use
	 * cases.
	 * </summary>
	 */
	internal static void RegisterExpenseUseCases(this IServiceCollection services)
	{
		services.AddTransient<CreateExpenseModelUseCase>();
		services.AddTransient<DeleteExpenseModelUseCase>();
		services.AddTransient<FormatExpenseModelsCSVUseCase>();
		services.AddTransient<ListExpenseModelUseCase>();
		services.AddTransient<UpdateExpenseModelUseCase>();
	}
}