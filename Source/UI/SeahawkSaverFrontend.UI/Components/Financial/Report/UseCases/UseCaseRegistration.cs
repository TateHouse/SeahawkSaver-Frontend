namespace SeahawkSaverFrontend.UI.Components.Financial.Report.UseCases;
/**
 * <summary>
 * A class for registering the <see cref="FinancialReportPage"/> related use cases.
 * </summary>
 */
public static class UseCaseRegistration
{
	/**
	 * <summary>
	 * An extension method for <see cref="IServiceCollection"/> to register the
	 * <see cref="FinancialReportPage"/> related use cases.
	 * </summary>
	 */
	internal static void RegisterFinancialReportUseCases(this IServiceCollection services)
	{
		services.AddTransient<CalculateAverageDebtPerMonthUseCase>();
		services.AddTransient<CalculateAverageExpensePerMonthUseCase>();
		services.AddTransient<CalculateAverageIncomePerMonthUseCase>();
		services.AddTransient<CalculateAverageSavingPerMonthUseCase>();
		services.AddTransient<CalculateAverageSubscriptionPerMonthUseCase>();
		services.AddTransient<CalculateCurrentYearMonthlyTotalsUseCase>();
		services.AddTransient<CalculateNetSavingsUseCase>();
		services.AddTransient<FilterCurrentYearMonthsByBreakingEven>();
		services.AddTransient<FilterCurrentYearMonthsByInTheBlackUseCase>();
		services.AddTransient<FilterCurrentYearMonthsByInTheRedUseCase>();
	}
}