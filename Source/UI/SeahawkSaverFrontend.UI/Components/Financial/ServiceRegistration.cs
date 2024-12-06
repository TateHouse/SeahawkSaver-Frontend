namespace SeahawkSaverFrontend.UI.Components.Financial;
using SeahawkSaverFrontend.Domain.Models.Financial;
using SeahawkSaverFrontend.UI.Components.Financial.Calendar;
using SeahawkSaverFrontend.UI.Components.Financial.Calendar.Items;
using SeahawkSaverFrontend.UI.Components.Financial.Calendar.UseCases;

/**
 * <summary>
 * A class for registering services.
 * </summary>
 */
public static class ServiceRegistration
{
	/**
	 * <summary>
	 * An extension method for <see cref="IServiceCollection"/> to register the financial component's related services.
	 * </summary>
	 */
	public static IServiceCollection RegisterFinancialComponentServices(this IServiceCollection services)
	{
		services.AddTransient<IFinancialModelCalendarItemFactory<DebtModel>, FinancialModelCalendarItemFactory<DebtModel, CreateDebtModelCalendarItemsUseCase>>();
		services.AddTransient<IFinancialModelCalendarItemFactory<ExpenseModel>, FinancialModelCalendarItemFactory<ExpenseModel, CreateExpenseModelCalendarItemsUseCase>>();
		services.AddTransient<IFinancialModelCalendarItemFactory<IncomeModel>, FinancialModelCalendarItemFactory<IncomeModel, CreateIncomeModelCalendarItemsUseCase>>();
		services.AddTransient<IFinancialModelCalendarItemFactory<SavingModel>, FinancialModelCalendarItemFactory<SavingModel, CreateSavingModelCalendarItemsUseCase>>();
		services.AddTransient<IFinancialModelCalendarItemFactory<SubscriptionModel>, FinancialModelCalendarItemFactory<SubscriptionModel, CreateSubscriptionModelCalendarItemsUseCase>>();
		services.AddTransient<FinancialManagementCalendarItemManager>();

		return services;
	}
}