using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Components.Financial.Calendar.Items.Create;
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
			Snackbar.Add("The date cannot be in the future and must be less than 30 days ago.");

			return;
		}

		propertyValidationStatuses[nameof(DateTime)] = true;
	}

	public async Task<bool> Create()
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
			return false;
		}

		ExpenseModelCache.Add(expenseModel);

		return true;
	}

	private bool IsValid()
	{
		if (expenseModel.Amount <= 0)
		{
			propertyValidationStatuses[nameof(ExpenseModel.Amount)] = false;
			Snackbar.Add("The amount must be greater than 0.");
		}
		else
		{
			propertyValidationStatuses[nameof(ExpenseModel.Amount)] = true;
		}

		return propertyValidationStatuses.All(property => property.Value);
	}
}