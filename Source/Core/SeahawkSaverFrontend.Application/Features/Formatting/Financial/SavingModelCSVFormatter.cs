namespace SeahawkSaverFrontend.Application.Features.Formatting.Financial;
using SeahawkSaverFrontend.Application.Abstractions.Formatting;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Saving.Formatting.DTOs;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A csv formatter for <see cref="SavingModel"/>.
 * </summary>
 */
public sealed class SavingModelCSVFormatter : ICSVFormatter<SavingModel, SavingModelCSVRow>
{
	public IReadOnlyList<SavingModelCSVRow> Format(IEnumerable<SavingModel> elements)
	{
		var rows = new List<SavingModelCSVRow>();

		foreach (var element in elements)
		{
			var row = new SavingModelCSVRow
			{
				Type = FinancialModelType.Saving,
				Amount = element.Amount,
				DateTime = element.DateTime
			};

			rows.Add(row);
		}

		return rows;
	}
}