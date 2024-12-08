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
		services.AddTransient<CalculateNetSavingsUseCase>();
	}
}