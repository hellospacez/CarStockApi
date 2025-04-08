namespace CarStockApi.Models;

public class Car {
    public int Id { get; set; }
    public string Make { get; set; } = default!;
    public string Model { get; set; } = default!;
    public int Year { get; set; }
    public int Stock { get; set; }
    public int DealerId { get; set; }
}