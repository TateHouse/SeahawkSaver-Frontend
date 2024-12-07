namespace SeahawkSaverFrontend.UI.Components.Financial.Calendar.Items.Download;
using SeahawkSaverFrontend.Application.Abstractions.Caching;
using SeahawkSaverFrontend.Application.Abstractions.UseCases;
using SeahawkSaverFrontend.Application.Abstractions.UseCases.Financial.Formatting;
using SeahawkSaverFrontend.Application.Abstractions.UseCases.Financial.Formatting.DTOs;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Debt.Formatting;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Debt.Formatting.DTOs;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Expense.Formatting;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Expense.Formatting.DTOs;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Income.Formatting;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Income.Formatting.DTOs;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Saving.Formatting;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Saving.Formatting.DTOs;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Subscription.Formatting;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Subscription.Formatting.DTOs;
using SeahawkSaverFrontend.Domain.Models.Financial;
using SeahawkSaverFrontend.Domain.Models.Utilities;
using System.Text;

/**
 * <summary>
 * A facade to manage formatting <see cref="FinancialModel"/> types into a csv format.
 * </summary>
 */
public sealed class FinancialModelCSVFormatterFacade
{
	private readonly IUseCaseFactory useCaseFactory;
	private readonly IUserCache userCache;

	/**
	 * <summary>
	 * Instantiates a new <see cref="FinancialModelCSVFormatterFacade"/> instance.
	 * </summary>
	 * <param name="useCaseFactory">The use case factory to use.</param>
	 * <param name="userCache">The user cache to use</param>
	 */
	public FinancialModelCSVFormatterFacade(IUseCaseFactory useCaseFactory,
											IUserCache userCache)
	{
		this.useCaseFactory = useCaseFactory;
		this.userCache = userCache;
	}

	/**
	 * <summary>
	 * Gets the file name.
	 * </summary>
	 * <returns>A string representation of the file name.</returns>
	 */
	public string GetFileName(DateRangeModel dateRangeModel)
	{
		return $"SeahawkSaver_{userCache.User.FirstName}{userCache.User.LastName}_FinancialData_{dateRangeModel.Start.ToShortDateString()}-{dateRangeModel.End.ToShortDateString()}.csv";
	}

	/**
	 * <summary>
	 * Formats the specified financial model types that are within the specified date range.
	 * </summary>
	 * <param name="financialModelTypes">The types of the financial models to include.</param>
	 * <param name="dateRangeModel">The date range.</param>
	 */
	public async Task<string> FormatAsync(IEnumerable<FinancialModelType> financialModelTypes, DateRangeModel dateRangeModel)
	{
		var unsortedRows = new List<FinancialModelCSVRow>();

		foreach (var financialModelType in financialModelTypes)
		{
			IEnumerable<FinancialModelCSVRow> rows = financialModelType switch
													 {
														 FinancialModelType.Debt => await FormatFinancialModelAsync<DebtModel, DebtModelCSVRow, FormatDebtModelsCSVUseCase>(dateRangeModel),
														 FinancialModelType.Expense => await FormatFinancialModelAsync<ExpenseModel, ExpenseModelCSVRow, FormatExpenseModelsCSVUseCase>(dateRangeModel),
														 FinancialModelType.Income => await FormatFinancialModelAsync<IncomeModel, IncomeModelCSVRow, FormatIncomeModelsCSVUseCase>(dateRangeModel),
														 FinancialModelType.Saving => await FormatFinancialModelAsync<SavingModel, SavingModelCSVRow, FormatSavingModelsCSVUseCase>(dateRangeModel),
														 FinancialModelType.Subscription => await FormatFinancialModelAsync<SubscriptionModel, SubscriptionModelCSVRow, FormatSubscriptionModelsCSVUseCase>(dateRangeModel),
														 var _ => throw new ArgumentOutOfRangeException()
													 };

			unsortedRows.AddRange(rows);
		}

		var sortedRows = unsortedRows.OrderBy(row => row.DateTime)
									 .ToList();

		var stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("Date,Amount,Type");

		foreach (var row in sortedRows)
		{
			stringBuilder.AppendLine($"{row.DateTime:MM/dd/yyyy},{GetRowAmount(row)},{GetRowType(row)}");
		}

		return stringBuilder.ToString();
	}

	/**
	 * <summary>
	 * Formats a specific type of financial model.
	 * </summary>
	 * <param name="dateRangeModel">The date range.</param>
	 * <typeparam name="TFinancialModel">The type of the financial model.</typeparam>
	 * <typeparam name="TFinancialModelCSVRow">The type of the financial model csv row.</typeparam>
	 * <typeparam name="TUseCase">The type of the use case.</typeparam>
	 * <returns>A read-only list of the type of formatted models within the specified date range.</returns>
	 */
	private async Task<IReadOnlyList<TFinancialModelCSVRow>> FormatFinancialModelAsync<TFinancialModel, TFinancialModelCSVRow, TUseCase>(DateRangeModel dateRangeModel)
		where TFinancialModel : FinancialModel
		where TFinancialModelCSVRow : FinancialModelCSVRow
		where TUseCase : FormatFinancialModelsCSVUseCase<TFinancialModel, TFinancialModelCSVRow>
	{
		var useCase = useCaseFactory.Create<TUseCase>();

		return await useCase.ExecuteAsync(dateRangeModel);
	}

	/**
	 * <summary>
	 * Gets the amount for the specified row.
	 * </summary>
	 * <param name="row">A row.</param>
	 * <returns>A string representation of the amount in the given row.</returns>
	 * <exception cref="ArgumentException">Thrown if the <see cref="FinancialModelType"/> is not valid.</exception>
	 */
	private static string GetRowAmount(FinancialModelCSVRow row)
	{
		return row switch
			   {
				   DebtModelCSVRow debtRow => debtRow.Amount.ToString("F"),
				   ExpenseModelCSVRow expenseRow => expenseRow.Amount.ToString("F"),
				   IncomeModelCSVRow incomeRow => incomeRow.Amount.ToString("F"),
				   SavingModelCSVRow savingRow => savingRow.Amount.ToString("F"),
				   SubscriptionModelCSVRow subscriptionRow => subscriptionRow.Amount.ToString("F"),
				   var _ => throw new ArgumentException("Invalid row type.")
			   };
	}

	/**
	 * <summary>
	 * Gets the type of the specified row.
	 * </summary>
	 * <param name="row">A row.</param>
	 * <returns>A string representation of the type of the given row.</returns>
	 * <exception cref="ArgumentException">Thrown if the <see cref="FinancialModelType"/> is not valid.</exception>
	 */
	private static string GetRowType(FinancialModelCSVRow row)
	{
		return row switch
			   {
				   DebtModelCSVRow => "Debt",
				   ExpenseModelCSVRow => "Expense",
				   IncomeModelCSVRow => "Income",
				   SavingModelCSVRow => "Saving",
				   SubscriptionModelCSVRow => "Subscription",
				   var _ => throw new ArgumentException("Invalid row type.")
			   };
	}
}