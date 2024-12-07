namespace SeahawkSaverFrontend.Application.Features.Formatting.Financial;
using SeahawkSaverFrontend.Application.Abstractions.Formatting;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Subscription.Formatting.DTOs;
using SeahawkSaverFrontend.Domain.Models.Financial;

/**
 * <summary>
 * A csv formatter for <see cref="SubscriptionModel"/>.
 * </summary>
 */
public sealed class SubscriptionModelCSVFormatter : ICSVFormatter<SubscriptionModel, SubscriptionModelCSVRow>
{
	public IReadOnlyList<SubscriptionModelCSVRow> Format(IEnumerable<SubscriptionModel> elements)
	{
		var rows = new List<SubscriptionModelCSVRow>();

		foreach (var element in elements)
		{
			var row = new SubscriptionModelCSVRow
			{
				Type = FinancialModelType.Subscription,
				Amount = element.Amount,
				DateTime = element.DateTime
			};

			rows.Add(row);
		}

		return rows;
	}
}