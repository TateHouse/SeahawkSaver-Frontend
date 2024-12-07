using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Components.Financial.Calendar.Items.Create;
using MudBlazor;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Subscription.Create;
using SeahawkSaverFrontend.Domain.Models.Financial;

public partial class FinancialManagementCalendarCreateSubscriptionModelComponent : ComponentBase, IFinancialManagementCalendarCreateFinancialModelComponent
{
	[Parameter]
	public DateTime DateTime { get; init; }

	private readonly Dictionary<string, bool> propertyValidationStatuses = new Dictionary<string, bool>
	{
		{ nameof(SubscriptionModel.Amount), false },
		{ nameof(SubscriptionModel.DateTime), false }
	};

	private readonly SubscriptionModel subscriptionModel = new SubscriptionModel
	{
		Id = Guid.Empty,
	};

	protected override void OnParametersSet()
	{
		subscriptionModel.DateTime = DateTime;

		if (DateTime > DateTime.UtcNow || DateTime < DateTime.UtcNow.AddDays(-30))
		{
			propertyValidationStatuses[nameof(DateTime)] = false;
			Snackbar.Add("The date cannot be in the future and must be less than 30 days ago.", Severity.Error);

			return;
		}

		propertyValidationStatuses[nameof(DateTime)] = true;
	}

	public async Task<bool> CreateAsync()
	{
		var isValid = IsValid();

		if (!isValid)
		{
			return false;
		}

		var useCase = UseCaseFactory.Create<CreateSubscriptionModelUseCase>();
		var wasCreated = await useCase.ExecuteAsync(subscriptionModel);

		if (!wasCreated)
		{
			Snackbar.Add($"Failed to add a subscription on {subscriptionModel.DateTime!.Value.ToShortDateString()}.", Severity.Error);

			return false;
		}

		Snackbar.Add($"Added a subscription of ${subscriptionModel.Amount} on {subscriptionModel.DateTime!.Value.ToShortDateString()}.", Severity.Success);

		return true;
	}

	private bool IsValid()
	{
		if (subscriptionModel.Amount <= 0)
		{
			propertyValidationStatuses[nameof(SubscriptionModel.Amount)] = false;
			Snackbar.Add("The amount must be greater than 0.", Severity.Error);
		}
		else
		{
			propertyValidationStatuses[nameof(SubscriptionModel.Amount)] = true;
		}

		return propertyValidationStatuses.All(property => property.Value);
	}
}