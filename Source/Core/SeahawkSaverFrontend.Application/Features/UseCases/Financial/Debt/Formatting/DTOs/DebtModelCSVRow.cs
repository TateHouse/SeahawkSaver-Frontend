namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Debt.Formatting.DTOs;
using SeahawkSaverFrontend.Application.Abstractions.UseCases.Financial.Formatting.DTOs;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A model containing the <see cref="DebtModel"/> properties to include in the csv content.
 * </summary>
 */
public sealed record DebtModelCSVRow : FinancialModelCSVRow
{
	public required decimal Amount { get; init; }
	public override required DateTime? DateTime { get; init; }
}