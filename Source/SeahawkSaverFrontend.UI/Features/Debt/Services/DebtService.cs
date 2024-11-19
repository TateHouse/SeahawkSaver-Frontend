namespace SeahawkSaverFrontend.UI.Features.Debt.Services;
using AutoMapper;
using SeahawkSaverFrontend.UI.Features.Caching.Services;
using SeahawkSaverFrontend.UI.Features.Debt.DTOs;
using System.Net.Http.Json;

public sealed class DebtService : IDebtService
{
	private readonly HttpClient httpClient;
	private readonly IMapper mapper;
	private readonly IDataCache dataCache;

	public DebtService(HttpClient httpClient, IMapper mapper, IDataCache dataCache)
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

	public async Task<IEnumerable<DebtModel>> GetDebtsAsync()
	{
		var url = $"http://localhost:5103/api/v1/debt/list/{dataCache.User.UserId}";
		var response = await httpClient.GetAsync(url);

		if (response.IsSuccessStatusCode == false)
		{
			// TODO: Log an error message.
			return new List<DebtModel>();
		}

		var content = await response.Content.ReadFromJsonAsync<ListDebtEndpointResponse>();

		if (content == null)
		{
			// TODO: Log an error message.
			return new List<DebtModel>();
		}

		return content.Debts.Select(debt => new DebtEntryModel
					  {
						  DebtId = debt.DebtId,
						  Amount = debt.Amount,
						  DateTime = debt.DateTime
					  })
					  .ToList();
	}

	public async Task<bool> AddDebtAsync(DebtModel model)
	{
		var url = $"http://localhost:5103/api/v1/debt/{dataCache.User.UserId}";
		var request = new
		{
			Debt = new
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

		var content = await response.Content.ReadFromJsonAsync<CreateDebtEndpointResponse>();

		if (content == null)
		{
			// TODO: Log an error message.
			return false;
		}

		model.DebtId = content.DebtId;

		return true;
	}

	public async Task<bool> UpdateDebtAsync(DebtModel model)
	{
		var url = $"http://localhost:5103/api/v1/debt/{dataCache.User.UserId}?debtId={model.DebtId}";
		var content = new
		{
			Debt = new
			{
				DebtId = model.DebtId,
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

	public async Task<bool> RemoveDebtAsync(DebtModel model)
	{
		var url = $"http://localhost:5103/api/v1/debt/{dataCache.User.UserId}?debtId={model.DebtId}";
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