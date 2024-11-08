using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Features.Subscription.Manage.Components;
using SeahawkSaverFrontend.UI.Features.Subscription.Manage.DTOs;

public partial class SubscriptionManagementComponent : ComponentBase
{
	private List<SubscriptionEntryModel> subscriptions = new List<SubscriptionEntryModel>();

	protected override async Task OnInitializedAsync()
	{
		subscriptions = (await SubscriptionService.GetSubscriptionsAsync())
						.Select(subscription => new SubscriptionEntryModel
						{
							SubscriptionId = subscription.SubscriptionId,
							Amount = subscription.Amount,
							DateTime = subscription.DateTime,
							IsEditable = false
						})
						.ToList();

		StateHasChanged();
	}

	private async Task Add()
	{

		var dialog = await DialogService.ShowAsync<CreateSubscriptionFormComponent>();
		var dialogResult = await dialog.Result;

		if (!dialogResult.Canceled && dialogResult.Data is SubscriptionEntryModel model)
		{
			var result = await SubscriptionService.AddSubscriptionAsync(model);

			if (result)
			{
				model.ErrorMessage = null;
				subscriptions.Add(model);
			}
			else
			{
				model.ErrorMessage = "An error occurred when adding the subscription...";
			}

			StateHasChanged();
		}
	}

	private async Task Save(SubscriptionEntryModel model)
	{
		var result = await SubscriptionService.UpdateSubscriptionAsync(model);
		model.ErrorMessage = result ? null : "An error occurred when updating the subscription...";
		StateHasChanged();
	}

	private async Task Delete(SubscriptionEntryModel model)
	{
		var result = await SubscriptionService.RemoveSubscriptionAsync(model);

		if (result)
		{
			model.ErrorMessage = null;
			subscriptions.Remove(model);
		}
		else
		{
			model.ErrorMessage = "An error occurred when deleting the subscription...";
		}

		StateHasChanged();
	}
}