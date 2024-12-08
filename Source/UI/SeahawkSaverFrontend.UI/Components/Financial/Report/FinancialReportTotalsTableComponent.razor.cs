using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Components.Financial.Report;
public partial class FinancialReportTotalsTableComponent : ComponentBase, IDisposable
{
	protected override void OnInitialized()
	{
		FinancialReportManager.OnStateChanged += StateHasChanged;
	}

	public void Dispose()
	{
		FinancialReportManager.OnStateChanged -= StateHasChanged;
	}
}