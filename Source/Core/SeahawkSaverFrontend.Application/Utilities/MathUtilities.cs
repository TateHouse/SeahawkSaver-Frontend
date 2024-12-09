namespace SeahawkSaverFrontend.Application.Utilities;
/**
 * <summary>
 * A collection of utility methods math related operations.
 * </summary>
 */
public static class MathUtilities
{
	/**
	 * <summary>
	 * Updates the smallest and largest numbers.
	 * </summary>
	 * <param name="value">A number.</param>
	 * <param name="smallest">The smallest number.</param>
	 * <param name="largest">The largest number.</param>
	 */
	public static void UpdateMinMax(decimal value, ref decimal smallest, ref decimal largest)
	{
		smallest = Math.Min(smallest, value);
		largest = Math.Max(largest, value);
	}
}