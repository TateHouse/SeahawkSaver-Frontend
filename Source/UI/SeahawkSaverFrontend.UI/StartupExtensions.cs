namespace SeahawkSaverFrontend.UI;
using Microsoft.AspNetCore.Authentication.Cookies;
using MudBlazor.Services;
using SeahawkSaverFrontend.Application.UnitTest;
using SeahawkSaverFrontend.UI.Components;

/**
 * <summary>
 * Extension methods for configuring the application's services and middleware.
 * </summary>
 */
public static class StartupExtensions
{
	/**
	 * <summary>
	 * An extension method for <see cref="WebApplicationBuilder"/> to configure the application's services.
	 * </summary>
	 * <returns>The <see cref="WebApplicationBuilder"/> instance.</returns>
	 */
	public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
	{
		builder.Configuration.Sources.Clear();
		builder.Configuration.AddJsonFile("AppSettings.json", false, true);
		builder.Configuration.AddJsonFile($"AppSettings.{builder.Environment.EnvironmentName}.json", true, true);
		builder.Configuration.AddUserSecrets<Program>();
		builder.Configuration.AddEnvironmentVariables();

		builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(configureOptions =>
		{
			configureOptions.SerializerOptions.PropertyNamingPolicy = null;
		});

		builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(configureOptions =>
		{
			configureOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
		});

		builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			   .AddCookie(configureOptions =>
			   {
				   configureOptions.LoginPath = "/login";
				   configureOptions.AccessDeniedPath = "/access-denied";
				   configureOptions.Cookie.HttpOnly = true;
				   configureOptions.ExpireTimeSpan = TimeSpan.FromMinutes(60);
				   configureOptions.SlidingExpiration = true;
			   });

		builder.Services.AddAuthorization();
		builder.Services.AddCascadingAuthenticationState();
		builder.Services.AddHttpClient();
		builder.Services
			   .AddRazorComponents()
			   .AddInteractiveServerComponents();

		builder.Services.AddMudServices();

		builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
		builder.Services.RegisterApplicationServices();

		return builder;
	}

	/**
	 * <summary>
	 * An extension method for <see cref="WebApplication"/> to configure the application's middleware.
	 * </summary>
	 * <returns>The <see cref="WebApplication"/> instance.</returns>
	 */
	public static WebApplication ConfigureMiddleware(this WebApplication application)
	{
		if (application.Environment.IsDevelopment() == false)
		{
			application.UseHsts();
		}

		application.UseDeveloperExceptionPage();
		application.UseHttpsRedirection();
		application.UseStaticFiles();
		application.UseRouting();
		application.UseAuthentication();
		application.UseAuthorization();
		application.UseAntiforgery();
		application.MapRazorComponents<App>()
				   .AddInteractiveServerRenderMode();

		return application;
	}
}