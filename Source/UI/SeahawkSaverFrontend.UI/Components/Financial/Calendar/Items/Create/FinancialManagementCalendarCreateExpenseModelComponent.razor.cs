using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Components.Financial.Calendar.Items.Create;
using MudBlazor;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Expense.Create;
using SeahawkSaverFrontend.Domain.Models.Financial;

public partial class FinancialManagementCalendarCreateExpenseModelComponent : ComponentBase, IFinancialManagementCalendarCreateFinancialModelComponent
{
	[Parameter]
	public DateTime DateTime { get; init; }

	private readonly Dictionary<string, bool> propertyValidationStatuses = new Dictionary<string, bool>
	{
		{ nameof(ExpenseModel.Amount), false },
		{ nameof(ExpenseModel.DateTime), false }
	};

	private readonly ExpenseModel expenseModel = new ExpenseModel
	{
		Id = Guid.Empty,
	};

	protected override void OnParametersSet()
	{
		expenseModel.DateTime = DateTime;

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

		var useCase = UseCaseFactory.Create<CreateExpenseModelUseCase>();
		var wasCreated = await useCase.ExecuteAsync(expenseModel);

		if (!wasCreated)
		{
			Snackbar.Add($"Failed to add an expense on {expenseModel.DateTime!.Value.ToShortDateString()}.", Severity.Error);

			return false;
		}

		Snackbar.Add($"Added an expense of ${expenseModel.Amount} on {expenseModel.DateTime!.Value.ToShortDateString()}.", Severity.Success);

		return true;
	}

	private bool IsValid()
	{
		if (expenseModel.Amount <= 0)
		{
			propertyValidationStatuses[nameof(ExpenseModel.Amount)] = false;
			Snackbar.Add("The amount must be greater than 0.", Severity.Error);
		}
		else
		{
			propertyValidationStatuses[nameof(ExpenseModel.Amount)] = true;
		}

		return propertyValidationStatuses.All(property => property.Value);
	}
}