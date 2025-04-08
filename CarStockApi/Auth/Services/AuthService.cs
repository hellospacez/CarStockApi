using System.Security.Claims;
using CarStockApi.Auth.Interfaces;
using CarStockApi.Auth.Requests;
using CarStockApi.Auth.Responses;
using CarStockApi.Data;
using Dapper;
using FastEndpoints.Security;
using Microsoft.Data.Sqlite;

namespace CarStockApi.Auth.Services;

public class AuthService : IAuthService
{
    public async Task RegisterAsync(RegisterRequestModel req)
    {
        using var conn = new SqliteConnection(Database.ConnectionString);

        try
        {
            await conn.ExecuteAsync(
                "INSERT INTO Dealers (Username, PasswordHash) VALUES (@Username, @PasswordHash)",
                new { req.Username, PasswordHash = req.Password }); // Not good, Haha
        }
        catch (SqliteException ex) when (ex.SqliteErrorCode == 19)
        {
            throw new Exception("Username already exists");
        }
    }

    public async Task<LoginResponseModel> LoginAsync(LoginRequestModel req)
    {
        using var conn = new SqliteConnection(Database.ConnectionString);

        var dealer = await conn.QuerySingleOrDefaultAsync<dynamic>(
            "SELECT * FROM Dealers WHERE Username = @Username",
            new { req.Username });

        if (dealer is null || (string)dealer.PasswordHash != req.Password)
            throw new Exception("Invalid username or password");

        var claims = new[]
        {
            new Claim("DealerId", dealer.Id.ToString()),
            new Claim(ClaimTypes.Name, dealer.Username)
        };

        var token = JWTBearer.CreateToken(
            signingKey: "super-secret-key-super-secret-key",
            expireAt: DateTime.UtcNow.AddDays(7),
            claims: claims,
            signingStyle: TokenSigningStyle.Symmetric
        );

        return new LoginResponseModel { Token = token };
    }
}