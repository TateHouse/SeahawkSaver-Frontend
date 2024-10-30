namespace SeahawkSaverFrontend.UI.Features.Income.Services;
using AutoMapper;
using SeahawkSaverFrontend.UI.Features.Caching.Services;
using SeahawkSaverFrontend.UI.Features.Income.DTOs;
using SeahawkSaverFrontend.UI.Features.Income.Manage.DTOs;
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

	public async Task<IEnumerable<IncomeModel>> GetIncomes()
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
			// TODO: Log an error.
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

	public Task<bool> UpdateIncome(IncomeModel model)
	{
		throw new NotImplementedException();
	}

	public Task<bool> RemoveIncome(IncomeModel model)
	{
		throw new NotImplementedException();
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