namespace SeahawkSaverFrontend.Application.Features.Formatting.Financial;
using SeahawkSaverFrontend.Application.Abstractions.Formatting;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Debt.Formatting.DTOs;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A csv formatter for <see cref="DebtModel"/>.
 * </summary>
 */
public sealed class DebtModelCSVFormatter : ICSVFormatter<DebtModel, DebtModelCSVRow>
{
	public IReadOnlyList<DebtModelCSVRow> Format(IEnumerable<DebtModel> elements)
	{
		var rows = new List<DebtModelCSVRow>();

		foreach (var element in elements)
		{
			var row = new DebtModelCSVRow
			{
				Type = FinancialModelType.Debt,
				Amount = element.Amount,
				DateTime = element.DateTime
			};

			rows.Add(row);
		}

		return rows;
	}
}