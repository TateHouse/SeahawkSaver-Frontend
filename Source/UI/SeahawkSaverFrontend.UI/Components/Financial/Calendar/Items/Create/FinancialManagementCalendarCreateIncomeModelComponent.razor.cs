using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Components.Financial.Calendar.Items.Create;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Income.Create;
using SeahawkSaverFrontend.Domain.Models.Financial;

public partial class FinancialManagementCalendarCreateIncomeModelComponent : ComponentBase, IFinancialManagementCalendarCreateFinancialModelComponent
{
	[Parameter]
	public DateTime DateTime { get; init; }

	private readonly Dictionary<string, bool> propertyValidationStatuses = new Dictionary<string, bool>
	{
		{ nameof(IncomeModel.Amount), false },
		{ nameof(IncomeModel.DateTime), false }
	};

	private readonly IncomeModel incomeModel = new IncomeModel
	{
		Id = Guid.Empty,
	};

	protected override void OnParametersSet()
	{
		incomeModel.DateTime = DateTime;

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

		var useCase = UseCaseFactory.Create<CreateIncomeModelUseCase>();
		var wasCreated = await useCase.ExecuteAsync(incomeModel);

		if (!wasCreated)
		{
			return false;
		}

		IncomeModelCache.Add(incomeModel);

		return true;
	}

	private bool IsValid()
	{
		if (incomeModel.Amount <= 0)
		{
			propertyValidationStatuses[nameof(IncomeModel.Amount)] = false;
			Snackbar.Add("The amount must be greater than 0.");
		}
		else
		{
			propertyValidationStatuses[nameof(IncomeModel.Amount)] = true;
		}

		return propertyValidationStatuses.All(property => property.Value);
	}
}