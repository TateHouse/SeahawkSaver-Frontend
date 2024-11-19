namespace SeahawkSaverFrontend.UI.Features.Saving.Services;
using AutoMapper;
using SeahawkSaverFrontend.UI.Features.Caching.Services;
using SeahawkSaverFrontend.UI.Features.Saving.DTOs;
using System.Net.Http.Json;

public sealed class SavingService : ISavingService
{
	private readonly HttpClient httpClient;
	private readonly IMapper mapper;
	private readonly IDataCache dataCache;

	public SavingService(HttpClient httpClient, IMapper mapper, IDataCache dataCache)
	{
		if (string.IsNullOrWhiteSpace(dataCache.Token))
		{
			throw new UnauthorizedAccessException();
		}

		this.httpClient = httpClient;
		this.mapper = mapper;
		this.dataCache = dataCache;

		AddBearerHeader();
	}

	public async Task<IEnumerable<SavingModel>> GetSavingsAsync()
	{
		var url = $"http://localhost:5103/api/v1/saving/list/{dataCache.User.UserId}";
		var response = await httpClient.GetAsync(url);

		if (response.IsSuccessStatusCode == false)
		{
			// TODO: Log an error message.
			return new List<SavingModel>();
		}

		var content = await response.Content.ReadFromJsonAsync<ListSavingEndpointResponse>();

		if (content == null)
		{
			// TODO: Log an error message.
			return new List<SavingModel>();
		}

		return content.Savings.Select(saving => new SavingModel
					  {
						  SavingId = saving.SavingId,
						  Amount = saving.Amount,
						  DateTime = saving.DateTime
					  })
					  .ToList();
	}

	public async Task<bool> AddSavingAsync(SavingModel model)
	{
		var url = $"http://localhost:5103/api/v1/saving/{dataCache.User.UserId}";
		var request = new
		{
			Saving = new
			{
				Amount = model.Amount,
				DateTime = model.DateTime
			}
		};

		var response = await httpClient.PostAsJsonAsync(url, request);

		if (response.IsSuccessStatusCode == false)
		{
			// TODO: Log an error message.
			return false;
		}

		var content = await response.Content.ReadFromJsonAsync<CreateSavingEndpointResponse>();

		if (content == null)
		{
			// TODO: Log an error message.
			return false;
		}

		model.SavingId = content.SavingId;

		return true;
	}

	public async Task<bool> UpdateSavingAsync(SavingModel model)
	{
		var url = $"http://localhost:5103/api/v1/saving/{dataCache.User.UserId}?savingId={model.SavingId}";
		var content = new
		{
			Saving = new
			{
				SavingId = model.SavingId,
				Amount = model.Amount,
				DateTime = model.DateTime
			}
		};

		var request = JsonContent.Create(content);
		var response = await httpClient.PutAsync(url, request);

		if (response.IsSuccessStatusCode == false)
		{
			// TODO: Log an error message.

			return false;
		}

		return true;
	}

	public async Task<bool> RemoveSavingAsync(SavingModel model)
	{
		var url = $"http://localhost:5103/api/v1/saving/{dataCache.User.UserId}?savingId={model.SavingId}";
		var response = await httpClient.DeleteAsync(url);

		if (response.IsSuccessStatusCode == false)
		{
			// TODO: Log an error message.

			return false;
		}

		return true;
	}

	private void AddBearerHeader()
	{
		if (httpClient.DefaultRequestHeaders.Contains("Bearer"))
		{
			return;
		}

		httpClient.DefaultRequestHeaders.Add("Bearer", dataCache.Token);
	}
}