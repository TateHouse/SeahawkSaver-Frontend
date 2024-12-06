using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Components.Financial.Calendar.Items.Manage;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Subscription.Delete;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Subscription.Update;
using SeahawkSaverFrontend.Domain.Models.Financial;

public partial class FinancialManagementCalendarManageSubscriptionModelComponent : ComponentBase, IFinancialManagementCalendarManageFinancialModelComponent
{
	[Parameter]
	public SubscriptionModel SubscriptionModel { get; set; } = null!;

	private readonly Dictionary<string, bool> propertyValidationStatuses = new Dictionary<string, bool>
	{
		{ nameof(SubscriptionModel.Amount), false },
		{ nameof(SubscriptionModel.DateTime), false }
	};

	private bool IsValid()
	{
		if (SubscriptionModel.Amount <= 0)
		{
			propertyValidationStatuses[nameof(SubscriptionModel.Amount)] = false;
			Snackbar.Add("The amount must be greater than 0.");
		}
		else
		{
			propertyValidationStatuses[nameof(SubscriptionModel.Amount)] = true;
		}

		if (SubscriptionModel.DateTime > DateTime.UtcNow || SubscriptionModel.DateTime < DateTime.UtcNow.AddDays(-30))
		{
			propertyValidationStatuses[nameof(DateTime)] = false;
			Snackbar.Add("The date cannot be in the future and must be less than 30 days ago.");

			propertyValidationStatuses[nameof(SubscriptionModel.DateTime)] = false;
		}
		else
		{
			propertyValidationStatuses[nameof(SubscriptionModel.DateTime)] = true;
		}

		return propertyValidationStatuses.All(property => property.Value);
	}

	public async Task<bool> UpdateAsync()
	{
		var isValid = IsValid();

		if (!isValid)
		{
			return false;
		}

		var useCase = UseCaseFactory.Create<UpdateSubscriptionModelUseCase>();
		var wasUpdated = await useCase.ExecuteAsync(SubscriptionModel);

		if (!wasUpdated)
		{
			return false;
		}

		SubscriptionModelCache.Update(SubscriptionModel);

		return true;
	}

	public async Task<bool> DeleteAsync()
	{
		var useCase = UseCaseFactory.Create<DeleteSubscriptionModelUseCase>();
		var wasDeleted = await useCase.ExecuteAsync(SubscriptionModel);

		if (!wasDeleted)
		{
			return false;
		}

		SubscriptionModelCache.Delete(SubscriptionModel);

		return true;
	}
}