using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Components.Financial.Calendar.Items.Download;
using Microsoft.JSInterop;
using MudBlazor;
using SeahawkSaverFrontend.Domain.Models.Financial;
using SeahawkSaverFrontend.Domain.Models.Utilities;

public partial class FinancialModelCSVDownloadComponent : ComponentBase
{
	private IEnumerable<string> selectedFinancialModelTypes = new HashSet<string>();
	private DateTime? start { get; set; }
	private DateTime? end { get; set; }

	[CascadingParameter]
	public MudDialogInstance Dialog { get; set; } = null!;

	private async Task OnDownloadAsync()
	{
		var dateRangeModel = new DateRangeModel
		{
			Start = start ?? DateTime.MinValue,
			End = end ?? DateTime.MaxValue
		};

		var financialModelTypes = new List<FinancialModelType>();

		foreach (var modelType in selectedFinancialModelTypes)
		{
			switch (modelType)
			{
				case nameof(FinancialModelType.Debt):
					financialModelTypes.Add(FinancialModelType.Debt);

					break;

				case nameof(FinancialModelType.Expense):
					financialModelTypes.Add(FinancialModelType.Expense);

					break;

				case nameof(FinancialModelType.Income):
					financialModelTypes.Add(FinancialModelType.Income);

					break;

				case nameof(FinancialModelType.Saving):
					financialModelTypes.Add(FinancialModelType.Saving);

					break;

				case nameof(FinancialModelType.Subscription):
					financialModelTypes.Add(FinancialModelType.Subscription);

					break;
			}
		}

		var content = await CSVFormatterFacade.FormatAsync(financialModelTypes, dateRangeModel);
		var fileName = CSVFormatterFacade.GetFileName(dateRangeModel);
		await JSRuntime.InvokeVoidAsync("downloadCsvFile", content, fileName);

		if (!selectedFinancialModelTypes.Any())
		{
			Snackbar.Add("No financial types selected, thus the csv file will contain no data.", Severity.Warning);
		}
		else
		{
			Snackbar.Add("Downloaded the csv file for the selected types within the specified date range.", Severity.Success);
		}

		Dialog.Close(true);
	}

	private void OnCancel()
	{
		Dialog.Close(false);
	}

	private string GetMultiSelectionText(List<string?>? selectedValues)
	{
		return $"{selectedValues.Count} financial model{(selectedValues.Count > 1 ? "s have" : " has")} been selected.";
	}
}