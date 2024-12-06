namespace SeahawkSaverFrontend.UI.Components.Financial.Calendar.UseCases;
/**
 * <summary>
 * A class for registering the <see cref="FinancialManagementCalendar"/> related use cases.
 * </summary>
 */
internal static class UseCaseRegistration
{
	/**
	 * <summary>
	 * An extension method for <see cref="IServiceCollection"/> to register the
	 * <see cref="FinancialManagementCalendar"/> related use cases.
	 * </summary>
	 */
	internal static void RegisterCalendarUseCases(this IServiceCollection services)
	{
		services.AddTransient<CreateDebtModelCalendarItemsUseCase>();
		services.AddTransient<CreateExpenseModelCalendarItemsUseCase>();
		services.AddTransient<CreateIncomeModelCalendarItemsUseCase>();
		services.AddTransient<CreateSavingModelCalendarItemsUseCase>();
		services.AddTransient<CreateSubscriptionModelCalendarItemsUseCase>();
	}
}