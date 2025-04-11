using CarStockApi.Endpoints.Car.Interfaces;
using CarStockApi.Endpoints.Car.Services;
using CarStockApi.Models.Request.Car;
using CarStockApi.Models.Response.Car;
using FluentAssertions;
using Moq;


namespace CarStockApiTests.Services;

public class CarServiceTests
{
    private readonly CarService _carService;
    private readonly Mock<ICarRepository> _carRepositoryMock;

    public CarServiceTests()
    {
        _carRepositoryMock = new Mock<ICarRepository>();
        _carService = new CarService(_carRepositoryMock.Object);
    }

    [Fact]
    public async Task AddCarAsync_Should_Call_Repository()
    {
        // Arrange
        var dealerId = 1;
        var request = new AddCarRequestModel
        {
            Make = "Audi",
            Model = "A4",
            Year = 2000,
            Stock = 5
        };

        // Act
        await _carService.AddCarAsync(dealerId, request);

        // Assert
        _carRepositoryMock.Verify(r => r.AddCarAsync(dealerId, request), Times.Once);
    }

    [Fact]
    public async Task GetCarsAsync_Should_Return_List()
    {
        // Arrange
        var dealerId = 1;
        var filter = new SearchCarsRequestModel
        {
            Make = "Audi"
        };

        var expected = new List<CarRecordModel>
        {
            new() { Id = 1, Make = "Audi", Model = "A4", Year = 2000, Stock = 5 }
        };

        _carRepositoryMock
            .Setup(r => r.GetCarsAsync(dealerId, filter))
            .ReturnsAsync(expected);

        // Act
        var result = await _carService.GetCarsAsync(dealerId, filter);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task UpdateCarStockAsync_Should_Return_True_When_Repository_Returns_True()
    {
        // Arrange
        var dealerId = 1;
        var carId = 10;
        var stock = 6;

        _carRepositoryMock
            .Setup(r => r.UpdateCarStockAsync(dealerId, carId, stock))
            .ReturnsAsync(true);

        // Act
        var result = await _carService.UpdateCarStockAsync(dealerId, carId, stock);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteCarAsync_Should_Return_False_When_Car_Not_Found()
    {
        // Arrange
        var dealerId = 1;
        var carId = 99;

        _carRepositoryMock
            .Setup(r => r.DeleteCarAsync(dealerId, carId))
            .ReturnsAsync(false);

        // Act
        var result = await _carService.DeleteCarAsync(dealerId, carId);

        // Assert
        result.Should().BeFalse();
    }
}
