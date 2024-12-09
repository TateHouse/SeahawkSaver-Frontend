namespace SeahawkSaverFrontend.Application.Utilities;
using SeahawkSaverFrontend.Domain.Models.Utilities;

/**
 * <summary>
 * A collection of utility methods for the <see cref="DateRangeModel"/>.
 * </summary>
 */
public static class DateRangeUtilities
{
	/**
	 * <summary>
	 * Checks if the date is within the specified range.
	 * </summary>
	 * <returns>True if the date is within the specified range. Otherwise, false.</returns>
	 */
	public static bool IsDateWithinDateRangeInclusive(DateTime dateTime, DateRangeModel dateRangeModel)
	{
		return dateTime.Date >= dateRangeModel.Start.Date && dateTime.Date <= dateRangeModel.End.Date;
	}
}