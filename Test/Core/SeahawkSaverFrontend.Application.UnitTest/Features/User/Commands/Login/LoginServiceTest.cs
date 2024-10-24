namespace SeahawkSaverFrontend.Application.UnitTest.Features.User.Commands.Login;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using SeahawkSaverFrontend.Application.Abstractions.Application;
using SeahawkSaverFrontend.Application.Features.User.Commands.Login;
using SeahawkSaverFrontend.Application.Features.User.Commands.Login.DTOs;
using SeahawkSaverFrontend.Application.UnitTest.Utilities;
using SeahawkSaverFrontend.Domain.Models;
using System.Net;
using JsonSerializer=System.Text.Json.JsonSerializer;

[TestFixture]
public sealed class LoginServiceTest
{
	private IConfiguration configuration;
	private Mock<IHttpClientFactory> mockHttpClientFactory;
	private Mock<IDataCache<User>> mockUserDataCache;
	private Mock<IDataCache<Token>> mockTokenDataCache;
	private LoginService loginService;

	[SetUp]
	public void SetUp()
	{
		var mapper = MapperFactory.Create<LoginServiceProfile>();
		var inMemorySettings = new Dictionary<string, string?>
		{
			{ "ApiSettings:Url", "http://localhost" }
		};

		var configurationBuilder = new ConfigurationBuilder();
		configurationBuilder.AddInMemoryCollection(inMemorySettings);

		configuration = configurationBuilder.Build();
		mockHttpClientFactory = new Mock<IHttpClientFactory>();
		mockUserDataCache = new Mock<IDataCache<User>>();
		mockTokenDataCache = new Mock<IDataCache<Token>>();
		loginService = new LoginService(configuration,
										mapper,
										mockHttpClientFactory.Object,
										mockUserDataCache.Object,
										mockTokenDataCache.Object);
	}

	[Test]
	public async Task GivenInvalidCredentials_WhenLoginAsync_ThenReturnsFalse()
	{
		var httpClient = LoginServiceTest.CreateHttpClient("", HttpStatusCode.NotFound);
		mockHttpClientFactory.Setup(mock => mock.CreateClient(It.IsAny<string>()))
							 .Returns(httpClient);

		mockUserDataCache.Setup(mock => mock.UpdateCache(It.IsAny<User>()));
		mockTokenDataCache.Setup(mock => mock.UpdateCache(It.IsAny<Token>()));

		var result = await loginService.LoginAsync("", "");

		Assert.That(result, Is.False);

		mockHttpClientFactory.Verify(mock => mock.CreateClient(It.IsAny<string>()), Times.Once);
		mockUserDataCache.Verify(mock => mock.UpdateCache(It.IsAny<User>()), Times.Never);
		mockTokenDataCache.Verify(mock => mock.UpdateCache(It.IsAny<Token>()), Times.Never);
	}

	[Test]
	public async Task GivenValidCredentials_WhenLoginAsync_ThenReturnsTrue()
	{
		var userId = Guid.NewGuid();
		const string email = "tracy.merril@gmail.com";

		var loginUserEndpointResponse = new LoginUserEndpointResponse
		{
			Token = "Token",
			User = new LoginUserEndpointUserResponse
			{
				UserId = userId,
				Email = email
			}
		};

		var jsonResponseContent = JsonSerializer.Serialize(loginUserEndpointResponse);
		var httpClient = LoginServiceTest.CreateHttpClient(jsonResponseContent, HttpStatusCode.OK);
		mockHttpClientFactory.Setup(mock => mock.CreateClient(It.IsAny<string>()))
							 .Returns(httpClient);

		mockUserDataCache.Setup(mock => mock.UpdateCache(It.IsAny<User>()));
		mockTokenDataCache.Setup(mock => mock.UpdateCache(It.IsAny<Token>()));

		var result = await loginService.LoginAsync(email, "#Password4Tracy");

		Assert.That(result, Is.True);

		mockHttpClientFactory.Verify(mock => mock.CreateClient(It.IsAny<string>()), Times.Once);
		mockUserDataCache.Verify(mock => mock.UpdateCache(It.IsAny<User>()), Times.Once);
		mockTokenDataCache.Verify(mock => mock.UpdateCache(It.IsAny<Token>()), Times.Once);
	}

	private static HttpClient CreateHttpClient(string jsonResponseContent, HttpStatusCode statusCode)
	{
		var httpResponseMessage = new HttpResponseMessage
		{
			StatusCode = statusCode,
			Content = new StringContent(jsonResponseContent)
		};

		var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
		mockHttpMessageHandler.Protected()
							  .Setup<Task<HttpResponseMessage>>("SendAsync",
																ItExpr.IsAny<HttpRequestMessage>(),
																ItExpr.IsAny<CancellationToken>())
							  .ReturnsAsync(httpResponseMessage);

		return new HttpClient(mockHttpMessageHandler.Object);
	}
}