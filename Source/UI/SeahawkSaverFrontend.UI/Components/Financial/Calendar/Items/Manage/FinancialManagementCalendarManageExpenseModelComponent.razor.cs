using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Components.Financial.Calendar.Items.Manage;
using MudBlazor;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Expense.Delete;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Expense.Update;
using SeahawkSaverFrontend.Domain.Models.Financial;

public partial class FinancialManagementCalendarManageExpenseModelComponent : ComponentBase, IFinancialManagementCalendarManageFinancialModelComponent
{
	[Parameter]
	public ExpenseModel ExpenseModel { get; set; } = null!;

	private readonly Dictionary<string, bool> propertyValidationStatuses = new Dictionary<string, bool>
	{
		{ nameof(ExpenseModel.Amount), false },
		{ nameof(ExpenseModel.DateTime), false }
	};

	private bool IsValid()
	{
		if (ExpenseModel.Amount <= 0)
		{
			propertyValidationStatuses[nameof(ExpenseModel.Amount)] = false;
			Snackbar.Add("The amount must be greater than 0.", Severity.Error);
		}
		else
		{
			propertyValidationStatuses[nameof(ExpenseModel.Amount)] = true;
		}

		if (ExpenseModel.DateTime > DateTime.UtcNow || ExpenseModel.DateTime < DateTime.UtcNow.AddDays(-30))
		{
			propertyValidationStatuses[nameof(DateTime)] = false;
			Snackbar.Add("The date cannot be in the future and must be less than 30 days ago.", Severity.Error);

			propertyValidationStatuses[nameof(ExpenseModel.DateTime)] = false;
		}
		else
		{
			propertyValidationStatuses[nameof(ExpenseModel.DateTime)] = true;
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

		var useCase = UseCaseFactory.Create<UpdateExpenseModelUseCase>();
		var wasUpdated = await useCase.ExecuteAsync(ExpenseModel);

		if (!wasUpdated)
		{
			Snackbar.Add($"Failed to update an expense on {ExpenseModel.DateTime!.Value.ToShortDateString()}.", Severity.Error);

			return false;
		}

		Snackbar.Add($"Updated an expense on {ExpenseModel.DateTime!.Value.ToShortDateString()}.", Severity.Success);

		return true;
	}

	public async Task<bool> DeleteAsync()
	{
		var useCase = UseCaseFactory.Create<DeleteExpenseModelUseCase>();
		var wasDeleted = await useCase.ExecuteAsync(ExpenseModel);

		if (!wasDeleted)
		{
			Snackbar.Add($"Failed to delete an expense on {ExpenseModel.DateTime!.Value.ToShortDateString()}.", Severity.Error);

			return false;
		}

		Snackbar.Add($"Deleted an expense on {ExpenseModel.DateTime!.Value.ToShortDateString()}.", Severity.Success);

		return true;
	}
}