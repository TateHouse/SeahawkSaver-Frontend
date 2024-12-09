namespace SeahawkSaverFrontend.Application.Abstractions.UseCases.Financial.Formatting.DTOs;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * An abstract base record for all financial model csv rows to derive from.
 * </summary>
 */
public abstract record FinancialModelCSVRow
{
	public abstract DateTime? DateTime { get; init; }
	public required FinancialModelType Type { get; init; }
}