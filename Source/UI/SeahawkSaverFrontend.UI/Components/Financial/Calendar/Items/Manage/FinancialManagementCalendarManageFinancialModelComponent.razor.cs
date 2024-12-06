using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Components.Financial.Calendar.Items.Manage;
using MudBlazor;
using SeahawkSaverFrontend.Domain.Models.Financial;

public partial class FinancialManagementCalendarManageFinancialModelComponent : ComponentBase
{
	private IFinancialManagementCalendarManageFinancialModelComponent financialModelComponent = null!;

	[Parameter]
	public FinancialModelType FinancialModelType { get; set; }

	[Parameter]
	public FinancialModel FinancialModel { get; set; } = null!;

	[CascadingParameter]
	public MudDialogInstance Dialog { get; set; } = null!;

	private async Task OnUpdateAsync()
	{
		var wasUpdated = await financialModelComponent.UpdateAsync();

		if (wasUpdated)
		{
			Dialog.Close(true);
		}
	}

	private async Task OnDeleteAsync()
	{
		var wasDeleted = await financialModelComponent.DeleteAsync();

		if (wasDeleted)
		{
			Dialog.Close(true);
		}
	}

	private void OnCancel()
	{
		Dialog.Close();
	}
}