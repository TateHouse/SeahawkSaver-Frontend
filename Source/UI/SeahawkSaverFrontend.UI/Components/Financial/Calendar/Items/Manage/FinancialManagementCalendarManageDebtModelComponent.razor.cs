using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Components.Financial.Calendar.Items.Manage;
using MudBlazor;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Debt.Delete;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Debt.Update;
using SeahawkSaverFrontend.Domain.Models.Financial;

public partial class FinancialManagementCalendarManageDebtModelComponent : ComponentBase, IFinancialManagementCalendarManageFinancialModelComponent
{
	[Parameter]
	public DebtModel DebtModel { get; set; } = null!;

	private readonly Dictionary<string, bool> propertyValidationStatuses = new Dictionary<string, bool>
	{
		{ nameof(DebtModel.Amount), false },
		{ nameof(DebtModel.DateTime), false }
	};

	private bool IsValid()
	{
		if (DebtModel.Amount <= 0)
		{
			propertyValidationStatuses[nameof(DebtModel.Amount)] = false;
			Snackbar.Add("The amount must be greater than 0.", Severity.Error);
		}
		else
		{
			propertyValidationStatuses[nameof(DebtModel.Amount)] = true;
		}

		if (DebtModel.DateTime > DateTime.UtcNow || DebtModel.DateTime < DateTime.UtcNow.AddDays(-30))
		{
			propertyValidationStatuses[nameof(DateTime)] = false;
			Snackbar.Add("The date cannot be in the future and must be less than 30 days ago.", Severity.Error);

			propertyValidationStatuses[nameof(DebtModel.DateTime)] = false;
		}
		else
		{
			propertyValidationStatuses[nameof(DebtModel.DateTime)] = true;
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

		var useCase = UseCaseFactory.Create<UpdateDebtModelUseCase>();
		var wasUpdated = await useCase.ExecuteAsync(DebtModel);

		if (!wasUpdated)
		{
			Snackbar.Add($"Failed to update a debt on {DebtModel.DateTime!.Value.ToShortDateString()}.", Severity.Error);

			return false;
		}

		Snackbar.Add($"Updated a debt on {DebtModel.DateTime!.Value.ToShortDateString()}.", Severity.Success);

		return true;
	}

	public async Task<bool> DeleteAsync()
	{
		var useCase = UseCaseFactory.Create<DeleteDebtModelUseCase>();
		var wasDeleted = await useCase.ExecuteAsync(DebtModel);

		if (!wasDeleted)
		{
			Snackbar.Add($"Failed to delete a debt on {DebtModel.DateTime!.Value.ToShortDateString()}.", Severity.Error);

			return false;
		}

		Snackbar.Add($"Deleted a debt on {DebtModel.DateTime!.Value.ToShortDateString()}.", Severity.Success);

		return true;
	}
}