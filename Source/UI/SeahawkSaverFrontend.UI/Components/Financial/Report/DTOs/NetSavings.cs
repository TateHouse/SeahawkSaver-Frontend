namespace SeahawkSaverFrontend.UI.Components.Financial.Report.DTOs;
/**
 * <summary>
 * A data transfer object containing information about the net savings.
 * </summary>
 */
public sealed record NetSavings
{
	public required decimal Amount { get; init; }
	public required decimal Percentage { get; init; }
}