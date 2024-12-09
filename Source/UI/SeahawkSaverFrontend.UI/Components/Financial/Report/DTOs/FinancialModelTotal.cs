namespace SeahawkSaverFrontend.UI.Components.Financial.Report.DTOs;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A data transfer object containing the total amount for a given <see cref="FinancialModelType"/>.
 * </summary>
 */
public sealed record FinancialModelTotal
{
	public required decimal Amount { get; init; }
	public required decimal SmallestAmount { get; init; }
	public required decimal LargestAmount { get; init; }
	public required int Count { get; init; }
	public required FinancialModelType Type { get; init; }
}