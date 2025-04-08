namespace CarStockApi.Models.Request.Car;

public class AddCarRequest {
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public int Stock { get; set; }
}