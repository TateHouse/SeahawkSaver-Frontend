using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Components.Financial.Calendar.Items.Manage;
using MudBlazor;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Saving.Delete;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Saving.Update;
using SeahawkSaverFrontend.Domain.Models.Financial;

public partial class FinancialManagementCalendarManageSavingModelComponent : ComponentBase, IFinancialManagementCalendarManageFinancialModelComponent
{
	[Parameter]
	public SavingModel SavingModel { get; set; } = null!;

	private readonly Dictionary<string, bool> propertyValidationStatuses = new Dictionary<string, bool>
	{
		{ nameof(SavingModel.Amount), false },
		{ nameof(SavingModel.DateTime), false }
	};

	private bool IsValid()
	{
		if (SavingModel.Amount <= 0)
		{
			propertyValidationStatuses[nameof(SavingModel.Amount)] = false;
			Snackbar.Add("The amount must be greater than 0.", Severity.Error);
		}
		else
		{
			propertyValidationStatuses[nameof(SavingModel.Amount)] = true;
		}

		if (SavingModel.DateTime > DateTime.UtcNow || SavingModel.DateTime < DateTime.UtcNow.AddDays(-30))
		{
			propertyValidationStatuses[nameof(DateTime)] = false;
			Snackbar.Add("The date cannot be in the future and must be less than 30 days ago.", Severity.Error);

			propertyValidationStatuses[nameof(SavingModel.DateTime)] = false;
		}
		else
		{
			propertyValidationStatuses[nameof(SavingModel.DateTime)] = true;
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

		var useCase = UseCaseFactory.Create<UpdateSavingModelUseCase>();
		var wasUpdated = await useCase.ExecuteAsync(SavingModel);

		if (!wasUpdated)
		{
			return false;
		}

		SavingModelCache.Update(SavingModel);
		Snackbar.Add($"Updated a saving on {SavingModel.DateTime!.Value.ToShortDateString()}.", Severity.Success);

		return true;
	}

	public async Task<bool> DeleteAsync()
	{
		var useCase = UseCaseFactory.Create<DeleteSavingModelUseCase>();
		var wasDeleted = await useCase.ExecuteAsync(SavingModel);

		if (!wasDeleted)
		{
			return false;
		}

		SavingModelCache.Delete(SavingModel);

		return true;
	}
}