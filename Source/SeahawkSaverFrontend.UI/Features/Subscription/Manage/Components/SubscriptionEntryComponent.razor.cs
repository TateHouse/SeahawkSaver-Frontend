using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Features.Subscription.Manage.Components;
using SeahawkSaverFrontend.UI.Features.Subscription.Manage.DTOs;

public partial class SubscriptionEntryComponent : ComponentBase
{
	[Parameter]
	public SubscriptionEntryModel Model { get; set; }

	[Parameter]
	public EventCallback<SubscriptionEntryModel> OnSave { get; set; }

	[Parameter]
	public EventCallback<SubscriptionEntryModel> OnDelete { get; set; }

	private void ToggleEdit()
	{
		if (Model.IsEditable)
		{
			OnSave.InvokeAsync(Model);
		}

		Model.IsEditable = !Model.IsEditable;
	}

	private void Delete()
	{
		OnDelete.InvokeAsync(Model);
		StateHasChanged();
	}
}