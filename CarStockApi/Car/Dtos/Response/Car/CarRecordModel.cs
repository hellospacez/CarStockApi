namespace CarStockApi.Models.Response.Car;

public partial class CarRecordModel {
    public int Id { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public int Stock { get; set; }
}
