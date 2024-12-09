namespace SeahawkSaverFrontend.Domain.Models.Utilities;
/**
 * <summary>
 * A model to represent a date range.
 * </summary>
 */
public sealed record DateRangeModel
{
	public required DateTime Start { get; init; }
	public required DateTime End { get; init; }
}