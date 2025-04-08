using CarStockApi.Auth.Interfaces;
using CarStockApi.Auth.Services;
using CarStockApi.Data;
using CarStockApi.Endpoints.Car.Interfaces;
using CarStockApi.Endpoints.Car.Services;
using FastEndpoints;
using FastEndpoints.Security;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthenticationJwtBearer(options =>
{
	options.SigningKey = "super-secret-key-super-secret-key";
});
builder.Services.AddAuthorization();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICarService, CarService>();


// DI FastEndpoints 
builder.Services.AddFastEndpoints();




var app = builder.Build();


Database.Init();

app.UseAuthentication();
app.UseAuthorization();

// Using FastEndpoints 
app.UseFastEndpoints();

app.MapGet("/", () => "Hello World!");

app.Run();