using EndSickness.Infrastructure;
using EndSickness.Persistance;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddPersistance();
builder.Services.AddInfrastructure();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHsts();
app.UseHttpsRedirection();

app.UseCors("AllowedPolicies");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
