using Microsoft.AspNetCore.Components;

namespace SeahawkSaverFrontend.UI.Components.Financial.Calendar.Items.Create;
using SeahawkSaverFrontend.Application.Features.UseCases.Financial.Saving.Create;
using SeahawkSaverFrontend.Domain.Models.Financial;

public partial class FinancialManagementCalendarCreateSavingModelComponent : ComponentBase, IFinancialManagementCalendarCreateFinancialModelComponent
{
	[Parameter]
	public DateTime DateTime { get; init; }

	private readonly Dictionary<string, bool> propertyValidationStatuses = new Dictionary<string, bool>
	{
		{ nameof(SavingModel.Amount), false },
		{ nameof(SavingModel.DateTime), false }
	};

	private readonly SavingModel savingModel = new SavingModel
	{
		Id = Guid.Empty,
	};

	protected override void OnParametersSet()
	{
		savingModel.DateTime = DateTime;

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

		var useCase = UseCaseFactory.Create<CreateSavingModelUseCase>();
		var wasCreated = await useCase.ExecuteAsync(savingModel);

		if (!wasCreated)
		{
			return false;
		}

		SavingModelCache.Add(savingModel);

		return true;
	}

	private bool IsValid()
	{
		if (savingModel.Amount <= 0)
		{
			propertyValidationStatuses[nameof(SavingModel.Amount)] = false;
			Snackbar.Add("The amount must be greater than 0.");
		}
		else
		{
			propertyValidationStatuses[nameof(SavingModel.Amount)] = true;
		}

		return propertyValidationStatuses.All(property => property.Value);
	}
}