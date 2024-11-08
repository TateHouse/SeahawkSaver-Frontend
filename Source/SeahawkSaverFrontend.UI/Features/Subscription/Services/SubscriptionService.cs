namespace SeahawkSaverFrontend.UI.Features.Subscription.Services;
using AutoMapper;
using SeahawkSaverFrontend.UI.Features.Caching.Services;
using SeahawkSaverFrontend.UI.Features.Subscription.DTOs;
using SeahawkSaverFrontend.UI.Features.Subscription.Manage.DTOs;
using System.Net.Http.Json;

public class SubscriptionService : ISubscriptionService
{

	private readonly HttpClient httpClient;
	private readonly IMapper mapper;
	private readonly IDataCache dataCache;

	public SubscriptionService(HttpClient httpClient, IMapper mapper, IDataCache dataCache)
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

	public async Task<IEnumerable<SubscriptionModel>> GetSubscriptionsAsync()
	{
		var url = $"http://localhost:5103/api/v1/subscription/list/{dataCache.User.UserId}";
		var response = await httpClient.GetAsync(url);

		if (response.IsSuccessStatusCode == false)
		{
			// TODO: Log an error message.
			return new List<SubscriptionModel>();
		}

		var content = await response.Content.ReadFromJsonAsync<ListSubscriptionEndpointResponse>();

		if (content == null)
		{
			// TODO: Log an error message.
			return new List<SubscriptionModel>();
		}

		return content.Subscriptions.Select(subscription => new SubscriptionModel
					  {
						  SubscriptionId = subscription.SubscriptionId,
						  Amount = subscription.Amount,
						  DateTime = subscription.DateTime
					  })
					  .ToList();
	}

	public async Task<bool> AddSubscriptionAsync(SubscriptionModel model)
	{
		var url = $"http://localhost:5103/api/v1/subscription/{dataCache.User.UserId}";
		var request = new
		{
			Subscription = new
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

		var content = await response.Content.ReadFromJsonAsync<CreateSubscriptionEndpointResponse>();

		if (content == null)
		{
			// TODO: Log an error message.
			return false;
		}

		model.SubscriptionId = content.SubscriptionId;

		return true;
	}

	public async Task<bool> UpdateSubscriptionAsync(SubscriptionModel model)
	{
		var url = $"http://localhost:5103/api/v1/subscription/{dataCache.User.UserId}?subscriptionId={model.SubscriptionId}";
		var content = new
		{
			Subscription = new
			{
				SubscriptionId = model.SubscriptionId,
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

	public async Task<bool> RemoveSubscriptionAsync(SubscriptionModel model)
	{
		var url = $"http://localhost:5103/api/v1/subscription/{dataCache.User.UserId}?subscriptionId={model.SubscriptionId}";
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