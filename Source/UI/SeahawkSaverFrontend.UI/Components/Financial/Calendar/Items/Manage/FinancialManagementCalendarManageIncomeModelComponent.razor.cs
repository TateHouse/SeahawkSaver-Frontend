using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Components.Financial.Calendar.Items.Manage;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Income.Delete;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Income.Update;
using SeahawkSaverFrontend.Domain.Models.Financial;

public partial class FinancialManagementCalendarManageIncomeModelComponent : ComponentBase, IFinancialManagementCalendarManageFinancialModelComponent
{
	[Parameter]
	public IncomeModel IncomeModel { get; set; } = null!;

	private readonly Dictionary<string, bool> propertyValidationStatuses = new Dictionary<string, bool>
	{
		{ nameof(IncomeModel.Amount), false },
		{ nameof(IncomeModel.DateTime), false }
	};

	private bool IsValid()
	{
		if (IncomeModel.Amount <= 0)
		{
			propertyValidationStatuses[nameof(IncomeModel.Amount)] = false;
			Snackbar.Add("The amount must be greater than 0.");
		}
		else
		{
			propertyValidationStatuses[nameof(IncomeModel.Amount)] = true;
		}

		if (IncomeModel.DateTime > DateTime.UtcNow || IncomeModel.DateTime < DateTime.UtcNow.AddDays(-30))
		{
			propertyValidationStatuses[nameof(DateTime)] = false;
			Snackbar.Add("The date cannot be in the future and must be less than 30 days ago.");

			propertyValidationStatuses[nameof(IncomeModel.DateTime)] = false;
		}
		else
		{
			propertyValidationStatuses[nameof(IncomeModel.DateTime)] = true;
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

		var useCase = UseCaseFactory.Create<UpdateIncomeModelUseCase>();
		var wasUpdated = await useCase.ExecuteAsync(IncomeModel);

		if (!wasUpdated)
		{
			return false;
		}

		IncomeModelCache.Update(IncomeModel);

		return true;
	}

	public async Task<bool> DeleteAsync()
	{
		var useCase = UseCaseFactory.Create<DeleteIncomeModelUseCase>();
		var wasDeleted = await useCase.ExecuteAsync(IncomeModel);

		if (!wasDeleted)
		{
			return false;
		}

		IncomeModelCache.Delete(IncomeModel);

		return true;
	}
}