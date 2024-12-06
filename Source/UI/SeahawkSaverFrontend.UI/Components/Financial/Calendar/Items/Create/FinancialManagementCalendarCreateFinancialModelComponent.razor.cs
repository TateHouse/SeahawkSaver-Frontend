using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Components.Financial.Calendar.Items.Create;
using MudBlazor;

public partial class FinancialManagementCalendarCreateFinancialModelComponent : ComponentBase
{
	private FinancialModelType? financialModelType = null;
	private IFinancialManagementCalendarCreateFinancialModelComponent financialModelComponent = null!;

	[CascadingParameter]
	public MudDialogInstance Dialog { get; set; } = null!;

	[Parameter]
	public DateTime DateTime { get; set; }

	private async Task OnCreate()
	{
		var wasCreated = await financialModelComponent.CreateAsync();

		if (wasCreated)
		{
			Dialog.Close(true);
		}
	}

	private void OnCancel()
	{
		Dialog.Close();
	}
}