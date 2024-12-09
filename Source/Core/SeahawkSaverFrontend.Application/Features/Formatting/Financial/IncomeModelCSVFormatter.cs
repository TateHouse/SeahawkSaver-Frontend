namespace SeahawkSaverFrontend.Application.Features.Formatting.Financial;
using SeahawkSaverFrontend.Application.Abstractions.Formatting;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Income.Formatting.DTOs;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A csv formatter for <see cref="IncomeModel"/>.
 * </summary>
 */
public sealed class IncomeModelCSVFormatter : ICSVFormatter<IncomeModel, IncomeModelCSVRow>
{
	public IReadOnlyList<IncomeModelCSVRow> Format(IEnumerable<IncomeModel> elements)
	{
		var rows = new List<IncomeModelCSVRow>();

		foreach (var element in elements)
		{
			var row = new IncomeModelCSVRow
			{
				Type = FinancialModelType.Income,
				Amount = element.Amount,
				DateTime = element.DateTime
			};

			rows.Add(row);
		}

		return rows;
	}
}