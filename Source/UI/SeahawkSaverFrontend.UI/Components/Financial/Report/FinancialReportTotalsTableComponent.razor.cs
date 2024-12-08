using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Components.Financial.Report;
using SeahawkSaverFrontend.UI.Components.Financial.Report.DTOs;

public partial class FinancialReportTotalsTableComponent : ComponentBase
{
	[Parameter]
	public IEnumerable<FinancialModelTotal> FinancialModelTotals { get; set; }
}