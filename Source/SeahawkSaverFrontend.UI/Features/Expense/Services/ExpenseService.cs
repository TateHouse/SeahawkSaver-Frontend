namespace SeahawkSaverFrontend.UI.Features.Expense.Services;
using SeahawkSaverFrontend.UI.Features.Caching.Services;
using SeahawkSaverFrontend.UI.Features.Expense.DTOs;
using System.Net.Http.Json;

public sealed class ExpenseService : IExpenseService
{
	private readonly HttpClient httpClient;
	private readonly IDataCache dataCache;

	public ExpenseService(HttpClient httpClient, IDataCache dataCache)
	{
		if (string.IsNullOrWhiteSpace(dataCache.Token))
		{
			throw new UnauthorizedAccessException();
		}

		this.httpClient = httpClient;
		this.dataCache = dataCache;

		AddBearerHeader();
	}

	public async Task<IEnumerable<ExpenseModel>> GetExpensesAsync()
	{
		var url = $"http://localhost:5103/api/v1/expense/list/{dataCache.User.UserId}";
		var response = await httpClient.GetAsync(url);

		if (response.IsSuccessStatusCode == false)
		{
			// TODO: Log an error message.

			return new List<ExpenseModel>();
		}

		var content = await response.Content.ReadFromJsonAsync<ListExpenseEndpointResponse>();

		if (content == null)
		{
			// TODO: Log an error message.

			return new List<ExpenseModel>();
		}

		return content.Expenses.Select(expense => new ExpenseEntryModel
					  {
						  ExpenseId = expense.ExpenseId,
						  Amount = expense.Amount,
						  DateTime = expense.DateTime
					  })
					  .ToList();
	}

	public async Task<bool> AddExpenseAsync(ExpenseModel model)
	{
		var url = $"http://localhost:5103/api/v1/expense/{dataCache.User.UserId}";
		var request = new
		{
			Expense = new
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

		var content = await response.Content.ReadFromJsonAsync<CreateExpenseEndpointResponse>();

		if (content == null)
		{
			// TODO: Log an error message.

			return false;
		}

		model.ExpenseId = content.ExpenseId;

		return true;
	}

	public async Task<bool> UpdateExpenseAsync(ExpenseModel model)
	{
		var url = $"http://localhost:5103/api/v1/expense/{dataCache.User.UserId}?expenseId={model.ExpenseId}";
		var content = new
		{
			Expense = new
			{
				ExpenseId = model.ExpenseId,
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

	public async Task<bool> RemoveExpenseAsync(ExpenseModel model)
	{
		var url = $"http://localhost:5103/api/v1/expense/{dataCache.User.UserId}?expenseId={model.ExpenseId}";
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