namespace CarStockApi.Models;

public class Dealer {
    public int Id { get; set; }
    public string Username { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
}