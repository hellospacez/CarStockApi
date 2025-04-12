using CarStockApi.Auth.Interfaces;
using CarStockApi.Auth.Services;
using CarStockApi.Data;
using CarStockApi.Endpoints.Auth.Repository;
using CarStockApi.Endpoints.Car.Interfaces;
using CarStockApi.Endpoints.Car.Repository;
using CarStockApi.Endpoints.Car.Services;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;


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
builder.Services.AddFastEndpoints()
        .SwaggerDocument();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  
            .AllowAnyHeader()  
            .AllowAnyMethod();  
    });
});



var app = builder.Build();
app.UseCors("AllowAll");

app.Use(async (context, next) =>
{
    if (context.Request.Method == HttpMethods.Options)
    {
        context.Response.StatusCode = 200;
        await context.Response.CompleteAsync();
        return;
    }
    await next();
});




var database = app.Services.GetRequiredService<Database>();
database.Init();

app.UseAuthentication();
app.UseAuthorization();

app.UseFastEndpoints()
    .UseSwaggerGen();

app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

app.Run();
