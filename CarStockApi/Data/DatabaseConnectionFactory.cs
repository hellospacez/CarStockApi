using Microsoft.Data.Sqlite;

namespace CarStockApi.Data;

public class DatabaseConnectionFactory
{
    private readonly string _connectionString;


    public DatabaseConnectionFactory(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }


    public string GetConnectionString()
    {
        return _connectionString;
    }


    public SqliteConnection CreateConnection()
    {
        return new SqliteConnection(_connectionString);
    }
}