using CarStockApi.Auth.Interfaces;
using CarStockApi.Data;
using CarStockApi.Models;
using Dapper;

namespace CarStockApi.Endpoints.Auth.Repository;

public class AuthRepository : IAuthRepository
{
    private readonly DatabaseConnectionFactory _connectionFactory;

    public AuthRepository(DatabaseConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Dealer> GetDealerByUsernameAsync(string username)
    {
        using var conn = _connectionFactory.CreateConnection();
        var dealer = await conn.QuerySingleOrDefaultAsync<Dealer>(
            "SELECT * FROM Dealers WHERE Username = @Username",
            new { Username = username });
        return dealer;
    }

    public async Task AddDealerAsync(string username, string passwordHash)
    {
        using var conn = _connectionFactory.CreateConnection();
        await conn.ExecuteAsync(
            "INSERT INTO Dealers (Username, PasswordHash) VALUES (@Username, @PasswordHash)",
            new { Username = username, PasswordHash = passwordHash });
    }
}