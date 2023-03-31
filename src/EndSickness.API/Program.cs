using EndSickness.Infrastructure;
using EndSickness.Persistance;
using EndSickness.Infrastructure.Middlewares;
using Serilog;

WebApplicationBuilder? builder = null!;

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
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

builder.Host.UseSerilog((ctx, cfg) => cfg.ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowedPolicies", corsBuilder =>
    {
        corsBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddPersistance(builder.Configuration);
builder.Services.AddInfrastructure();


var app = builder.Build();

app.GlobalErrorHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHsts();
app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseRouting();
app.UseCors("AllowedPolicies");

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
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
