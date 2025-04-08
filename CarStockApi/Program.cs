using CarStockApi.Auth.Interfaces;
using CarStockApi.Auth.Services;
using CarStockApi.Data;
using CarStockApi.Endpoints.Auth.Repository;
using CarStockApi.Endpoints.Car.Interfaces;
using CarStockApi.Endpoints.Car.Repository;
using CarStockApi.Endpoints.Car.Services;
using FastEndpoints;
using FastEndpoints.Security;


var builder = WebApplication.CreateBuilder(args);



builder.Services.AddSingleton<DatabaseConnectionFactory>();
builder.Services.AddSingleton<Database>(); 
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddAuthenticationJwtBearer(options =>
{
    options.SigningKey = "super-secret-key-super-secret-key";
});
builder.Services.AddAuthorization();
builder.Services.AddFastEndpoints();




var app = builder.Build();

var database = app.Services.GetRequiredService<Database>();
database.Init();

app.UseAuthentication();
app.UseAuthorization();

app.UseFastEndpoints();

app.MapGet("/", () => "Hello World!");

app.Run();