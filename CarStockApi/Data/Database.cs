namespace CarStockApi.Data;
using Microsoft.Data.Sqlite;
using Dapper;


public class Database
{
    public const string ConnectionString = "Data Source=carstock.db";

    public static void Init()
    {
        
        if (!File.Exists("carstock.db"))
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var sql = @"
                CREATE TABLE Dealers (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Username TEXT NOT NULL UNIQUE,
                    PasswordHash TEXT NOT NULL
                );

                CREATE TABLE Cars (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Make TEXT NOT NULL,
                    Model TEXT NOT NULL,
                    Year INTEGER NOT NULL,
                    Stock INTEGER NOT NULL,
                    DealerId INTEGER NOT NULL,
                    FOREIGN KEY (DealerId) REFERENCES Dealers(Id)
                );";

            connection.Execute(sql);
        }

        
        InitializeDefaultDealer();
    }

    private static void InitializeDefaultDealer()
    {
        using var conn = new SqliteConnection(ConnectionString);

      
        var existingDealer = conn.QuerySingleOrDefault<dynamic>(
            "SELECT * FROM Dealers WHERE Username = @Username", 
            new { Username = "dealer1" });

        if (existingDealer == null)
        {
            
            var defaultPassword = "password123"; 
            conn.Execute(
                "INSERT INTO Dealers (Username, PasswordHash) VALUES (@Username, @PasswordHash)", 
                new { Username = "dealer1", PasswordHash = defaultPassword });

            Console.WriteLine("Default dealer 'dealer1' created.");
        }
    }
    
}