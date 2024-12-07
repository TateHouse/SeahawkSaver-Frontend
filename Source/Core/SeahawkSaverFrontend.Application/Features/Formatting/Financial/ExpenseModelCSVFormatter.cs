namespace SeahawkSaverFrontend.Application.Features.Formatting.Financial;
using SeahawkSaverFrontend.Application.Abstractions.Formatting;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Expense.Formatting.DTOs;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A csv formatter for <see cref="ExpenseModel"/>.
 * </summary>
 */
public sealed class ExpenseModelCSVFormatter : ICSVFormatter<ExpenseModel, ExpenseModelCSVRow>
{
	public IReadOnlyList<ExpenseModelCSVRow> Format(IEnumerable<ExpenseModel> elements)
	{
		var rows = new List<ExpenseModelCSVRow>();

		foreach (var element in elements)
		{
			var row = new ExpenseModelCSVRow
			{
				Type = FinancialModelType.Expense,
				Amount = element.Amount,
				DateTime = element.DateTime
			};

			rows.Add(row);
		}

		return rows;
	}
}