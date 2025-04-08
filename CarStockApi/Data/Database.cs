using CarStockApi.Models;

namespace CarStockApi.Data;
using Microsoft.Data.Sqlite;
using Dapper;


public class Database
{
    
    
    private readonly DatabaseConnectionFactory _connectionFactory;

    public Database(DatabaseConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    

    public void Init()
    {
        using var connection = _connectionFactory.CreateConnection();
        
        
        if (!File.Exists("carstock.db"))
        {
            CreateDatabaseTables(connection);
        }
        
        InitializeDefaultDealer(connection);
    }

        private void CreateDatabaseTables(SqliteConnection connection)
        {
       

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
        

        
        private void InitializeDefaultDealer(SqliteConnection connection)
        {
   
  
            var existingDealer = connection.QuerySingleOrDefault<Dealer>(
                "SELECT * FROM Dealers WHERE Username = @Username", 
                new { Username = "dealer1" });

            if (existingDealer == null)
            {
                var defaultPassword = "password123"; 
         
                connection.Execute(
                    "INSERT INTO Dealers (Username, PasswordHash) VALUES (@Username, @PasswordHash)", 
                    new { Username = "dealer1", PasswordHash = defaultPassword });

                Console.WriteLine("Default dealer 'dealer1' created.");
            }
        }
    
}