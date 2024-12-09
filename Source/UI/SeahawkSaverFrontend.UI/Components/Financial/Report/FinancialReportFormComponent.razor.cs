using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Components.Financial.Report;
using MudBlazor;
using SeahawkSaverFrontend.Domain.Models.Utilities;

public partial class FinancialReportFormComponent : ComponentBase
{
	private DateTime? start { get; set; }
	private DateTime? end { get; set; }

	[CascadingParameter]
	public MudDialogInstance Dialog { get; set; } = null!;

	private async Task OnClick_Generate()
	{
		var dateRangeModel = new DateRangeModel
		{
			Start = start ?? DateTime.MinValue,
			End = end ?? DateTime.MaxValue
		};

		await FinancialReportManager.GenerateAsync(dateRangeModel);
		Dialog.Close(true);
	}

	private void OnClick_Cancel()
	{
		Dialog.Close(false);
	}
}