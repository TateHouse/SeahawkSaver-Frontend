namespace SeahawkSaverFrontend.UI.Components.Financial.Report.UseCases;
/**
 * <summary>
 * A use case to filter the current year monthly totals to the months that were in the black.
 * </summary>
 */
public sealed class FilterCurrentYearMonthsByInTheBlackUseCase : FilterCurrentYearMonthsByCashFlow
{
	protected override bool DoesMonthMeetCashFlowType(decimal cashFlow)
	{
		return cashFlow > 0;
	}
}