namespace SeahawkSaverFrontend.Application.Features.UseCases.Financial.Expense.Formatting.DTOs;
using SeahawkSaverFrontend.Application.Abstractions.UseCases.Financial.Formatting.DTOs;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A model containing the <see cref="ExpenseModel"/> properties to include in the csv content.
 * </summary>
 */
public sealed record ExpenseModelCSVRow : FinancialModelCSVRow
{
	public required decimal Amount { get; init; }
	public override required DateTime? DateTime { get; init; }
}