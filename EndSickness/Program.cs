using EndSickness.Data.JsonConverters;
using EndSickness.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using System.Globalization;
using EndSickness.Middlewares;
using EndSickness.Exceptions;
using EndSickness.Exceptions.Interfaces;
using EndSickness.ExceptionsHandling.ExceptionHandlingStrategy;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient("EndSickness", httpClient =>
{
    httpClient.BaseAddress = new Uri(builder.Configuration.GetValue<string>("EndSicknessAPI"));
    httpClient.Timeout = TimeSpan.FromMinutes(1);
});
builder.Services.AddHttpClient("IS4", httpClient =>
{
    httpClient.BaseAddress = new Uri(builder.Configuration.GetValue<string>("IS4"));
    httpClient.Timeout = TimeSpan.FromMinutes(1);
});
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

JsonConvert.DefaultSettings = () => new JsonSerializerSettings
{
    Culture = CultureInfo.GetCultureInfoByIetfLanguageTag("pl-PL"),
    Converters = new List<JsonConverter> { new DateOnlyConverter(), new TimeOnlyConverter()}
};

JwtSecurityTokenHandler.DefaultMapInboundClaims = true;

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})
    .AddCookie("Cookies")
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = "https://localhost:5001";

        options.ClientId = "mvc";
        options.ClientSecret = "secret";
        options.ResponseType = "code";
        options.Scope.Add("api1");
        options.Scope.Add("profile");
        options.Scope.Add("offline_access");
        options.GetClaimsFromUserInfoEndpoint = true;
        options.SaveTokens = true;
        options.ClaimActions.MapUniqueJsonKey("Id", "Id");
        options.ClaimActions.MapUniqueJsonKey("Email", "Email");
        options.ClaimActions.MapUniqueJsonKey("Name", "Name");
        options.ClaimActions.MapUniqueJsonKey("Username", "Username");
    }); 

builder.Services.AddTransient<IExceptionResponse, ExceptionResponse>();
builder.Services.AddTransient<IExceptionHandlingStrategy, DefaultExceptionHandlingStrategy>();
builder.Services.AddTransient<ICurrentUserService, CurrentUserService>();
builder.Services.AddTransient<IEndSicknessClient, EndSicknessClient>();


var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("default","{area=App}/{controller=Home}/{action=Index}/{id?}")
        .RequireAuthorization();
});
app.Run();
