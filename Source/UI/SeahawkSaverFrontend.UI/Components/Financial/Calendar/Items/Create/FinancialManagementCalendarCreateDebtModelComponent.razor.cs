using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Components.Financial.Calendar.Items.Create;
using MudBlazor;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Debt.Create;
using SeahawkSaverFrontend.Domain.Models.Financial;

public partial class FinancialManagementCalendarCreateDebtModelComponent : ComponentBase, IFinancialManagementCalendarCreateFinancialModelComponent
{
	[Parameter]
	public DateTime DateTime { get; init; }

	private readonly Dictionary<string, bool> propertyValidationStatuses = new Dictionary<string, bool>
	{
		{ nameof(DebtModel.Amount), false },
		{ nameof(DebtModel.DateTime), false }
	};

	private readonly DebtModel debtModel = new DebtModel
	{
		Id = Guid.Empty,
	};

	protected override void OnParametersSet()
	{
		debtModel.DateTime = DateTime;

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

		var useCase = UseCaseFactory.Create<CreateDebtModelUseCase>();
		var wasCreated = await useCase.ExecuteAsync(debtModel);

		if (!wasCreated)
		{
			return false;
		}

		DebtModelCache.Add(debtModel);
		Snackbar.Add($"Added ${debtModel.Amount} of debt on {debtModel.DateTime!.Value.ToShortDateString()}.", Severity.Success);

		return true;
	}

	private bool IsValid()
	{
		if (debtModel.Amount <= 0)
		{
			propertyValidationStatuses[nameof(DebtModel.Amount)] = false;
			Snackbar.Add("The amount must be greater than 0.", Severity.Error);
		}
		else
		{
			propertyValidationStatuses[nameof(DebtModel.Amount)] = true;
		}

		return propertyValidationStatuses.All(property => property.Value);
	}
}