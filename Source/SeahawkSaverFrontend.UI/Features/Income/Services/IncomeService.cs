namespace SeahawkSaverFrontend.UI.Features.Income.Services;
using AutoMapper;
using SeahawkSaverFrontend.UI.Features.Caching.Services;
using SeahawkSaverFrontend.UI.Features.Income.DTOs;
using System.Net.Http.Json;

public sealed class IncomeService : IIncomeService
{
	private readonly HttpClient httpClient;
	private readonly IMapper mapper;
	private readonly IDataCache dataCache;

	public IncomeService(HttpClient httpClient,
						 IMapper mapper,
						 IDataCache dataCache)
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

	public async Task<IEnumerable<IncomeModel>> GetIncomesAsync()
	{
		var url = $"http://localhost:5103/api/v1/income/list/{dataCache.User.UserId}";
		var response = await httpClient.GetAsync(url);

		if (response.IsSuccessStatusCode == false)
		{
			// TODO: Log an error message.
			return new List<IncomeModel>();
		}

		var content = await response.Content.ReadFromJsonAsync<ListIncomeEndpointResponse>();

		if (content == null)
		{
			// TODO: Log an error message.
			return new List<IncomeModel>();
		}

		return content.Incomes.Select(income => new IncomeEntryModel
					  {
						  IncomeId = income.IncomeId,
						  Amount = income.Amount,
						  DateTime = income.DateTime
					  })
					  .ToList();
	}

	public async Task<bool> AddIncomeAsync(IncomeModel model)
	{
		var url = $"http://localhost:5103/api/v1/income/{dataCache.User.UserId}";
		var request = new
		{
			Income = new
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

		var content = await response.Content.ReadFromJsonAsync<CreateIncomeEndpointResponse>();

		if (content == null)
		{
			// TODO: Log an error message.
			return false;
		}

		model.IncomeId = content.IncomeId;

		return true;
	}

	public async Task<bool> UpdateIncomeAsync(IncomeModel model)
	{
		var url = $"http://localhost:5103/api/v1/income/{dataCache.User.UserId}?incomeId={model.IncomeId}";
		var content = new
		{
			Income = new
			{
				IncomeId = model.IncomeId,
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

	public async Task<bool> RemoveIncomeAsync(IncomeModel model)
	{
		var url = $"http://localhost:5103/api/v1/income/{dataCache.User.UserId}?incomeId={model.IncomeId}";
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