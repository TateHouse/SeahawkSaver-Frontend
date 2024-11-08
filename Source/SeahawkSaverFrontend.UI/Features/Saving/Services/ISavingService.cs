namespace SeahawkSaverFrontend.UI.Features.Saving.Services;
using SeahawkSaverFrontend.UI.Features.Saving.DTOs;

public interface ISavingService
{
	public Task<IEnumerable<SavingModel>> GetSavingsAsync();

	public Task<bool> AddSavingAsync(SavingModel model);

	public Task<bool> UpdateSavingAsync(SavingModel model);

	public Task<bool> RemoveSavingAsync(SavingModel model);
}