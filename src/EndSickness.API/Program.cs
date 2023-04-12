using EndSickness.Shared;
using EndSickness.Infrastructure;
using EndSickness.Persistance;
using EndSickness.Infrastructure.Middlewares;
using Serilog;
using Microsoft.OpenApi.Models;
using EndSickness.API;
using Microsoft.Extensions.DependencyInjection.Extensions;
using EndSickness.Application;
using EndSickness.API.Services;

WebApplicationBuilder? builder = null!;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false, true)
    .Build();
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

try
{
    Log.Information("Application is starting up");
    builder = WebApplication.CreateBuilder(args);
}
catch(Exception ex)
{
    Log.Fatal(ex, "Could not start up application");
    return;
}
finally
{
    Log.CloseAndFlush();
}

var apiUrl = builder.Configuration.GetSection("AppSettings").GetValue(typeof(string), "API_URL").ToString();
var authUrl = builder.Configuration.GetSection("AppSettings").GetValue(typeof(string), "AUTH_URL").ToString();

builder.Host.UseSerilog((ctx, cfg) => cfg.ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowedPolicies", corsBuilder =>
    {
        corsBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});
builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
{
    options.Authority = authUrl;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateAudience = false
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "api1");
    });
});
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,

        Flows = new OpenApiOAuthFlows()
        {

            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri($"{authUrl}/connect/authorize"),
                TokenUrl = new Uri($"{authUrl}/connect/token"),
                Scopes = new Dictionary<string, string>
                {
                    {"api1", "Demo - full access" }
                }
            }
        }
    });
    c.OperationFilter<AuthorizeCheckOperationFilter>();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "EndTrip API", Version = "v1" });
});
builder.Services.AddEndpointsApiExplorer();

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

builder.Services.AddShared();
builder.Services.AddApplication();
builder.Services.AddPersistance(builder.Configuration);
builder.Services.AddInfrastructure();


var app = builder.Build();

app.GlobalErrorHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.OAuthClientId("swagger");
        c.OAuth2RedirectUrl($"{apiUrl}/swagger/oauth2-redirect.html");
        c.OAuthUsePkce();
    });
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseRouting();
app.UseCors("AllowedPolicies");

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers().RequireAuthorization("ApiScope");
});

try
{
    Log.Information("Application is running");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
