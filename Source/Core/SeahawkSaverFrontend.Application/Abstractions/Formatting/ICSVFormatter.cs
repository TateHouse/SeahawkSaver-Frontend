namespace SeahawkSaverFrontend.Application.Abstractions.Formatting;
/**
 * <summary>
 * An interface for formatting data into the CSV format.
 * </summary>
 * <typeparam name="TData">The type of the data to format.</typeparam>
 * <typeparam name="TFormattedData">The type of the formatted data.</typeparam>
 */
public interface ICSVFormatter<in TData, out TFormattedData>
{
	/**
	 * <summary>
	 * Formats the specified data.
	 * </summary>
	 * <returns>A read-only list of the formatted data.</returns>
	 */
	public IReadOnlyList<TFormattedData> Format(IEnumerable<TData> elements);
}