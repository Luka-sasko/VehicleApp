using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq.Expressions;
using VehicleApp.Common;
using VehicleApp.Model;
using VehicleApp.Model.Common;
using VehicleApp.Service.Common;
using VehicleApp.WebApi.Controllers;

public class VehicleModelControllerTests
{
    private readonly Mock<IVehicleModelService> _vehicleModelServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly VehicleModelController _controller;

    public VehicleModelControllerTests()
    {
        _vehicleModelServiceMock = new Mock<IVehicleModelService>();
        _mapperMock = new Mock<IMapper>();
        _controller = new VehicleModelController(_vehicleModelServiceMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnOkResult_WhenVehicleModelsExist()
    {
        // Arrange
        var vehicleModels = new PagedList<VehicleModelView>(
            new List<VehicleModelView>
            {
                new VehicleModelView { Id = Guid.NewGuid(), Name = "X5" },
                new VehicleModelView { Id = Guid.NewGuid(), Name = "X3" }
            },
            1, 10, 2
        );
        _vehicleModelServiceMock.Setup(service => service.GetAllAsync(It.IsAny<Expression<Func<VehicleModel, bool>>>(), It.IsAny<Paging>(), It.IsAny<Sorting>()))
            .ReturnsAsync(vehicleModels);

        // Act
        var result = await _controller.GetAllAsync();

        // Assert
        var actionResult = Assert.IsType<ActionResult<PagedList<VehicleModelView>>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var returnedValue = Assert.IsType<PagedList<VehicleModelView>>(okResult.Value);
        returnedValue.Items.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnNotFound_WhenNoVehicleModelsExist()
    {
        // Arrange
        var vehicleModels = new PagedList<VehicleModelView>(new List<VehicleModelView>(), 1, 10, 0);
        _vehicleModelServiceMock.Setup(service => service.GetAllAsync(It.IsAny<Expression<Func<VehicleModel, bool>>>(), It.IsAny<Paging>(), It.IsAny<Sorting>()))
            .ReturnsAsync(vehicleModels);

        // Act
        var result = await _controller.GetAllAsync();

        // Assert
        var actionResult = Assert.IsType<ActionResult<PagedList<VehicleModelView>>>(result);
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        notFoundResult.Value.Should().Be("No vehicle models found.");
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNotFound_WhenVehicleModelDoesNotExist()
    {
        // Arrange
        var modelId = Guid.NewGuid();

        _vehicleModelServiceMock.Setup(service => service.GetVehicleModelByIdAsync(modelId))
            .ReturnsAsync((VehicleModelView)null);  

        // Act
        var result = await _controller.GetByIdAsync(modelId);

        // Assert
        var actionResult = Assert.IsType<ActionResult<VehicleModelView>>(result);
        var notFoundResult = Assert.IsType<NotFoundResult>(actionResult.Result);  
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnCreatedResult_WhenDataIsValid()
    {
        // Arrange
        var vehicleModelView = new VehicleModelView { Name = "X5", Abrv = "X", MakeId = Guid.NewGuid() };
        var vehicleModel = new VehicleModel { Id = Guid.NewGuid(), Name = "X5", Abrv = "X" };

        _mapperMock.Setup(mapper => mapper.Map<VehicleModel>(vehicleModelView))
            .Returns(vehicleModel);

        _vehicleModelServiceMock.Setup(service => service.AddVehicleModelAsync(vehicleModel))
            .Returns(Task.CompletedTask);

        _mapperMock.Setup(mapper => mapper.Map<VehicleModelView>(vehicleModel))
            .Returns(new VehicleModelView { Id = vehicleModel.Id, Name = vehicleModel.Name });

        // Act
        var result = await _controller.CreateAsync(vehicleModelView);

        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result); 
        var returnedValue = Assert.IsType<VehicleModelView>(actionResult.Value);
        returnedValue.Name.Should().Be("X5");
    }


    [Fact]
    public async Task UpdateAsync_ShouldReturnOkResult_WhenVehicleModelUpdated()
    {
        // Arrange
        var modelId = Guid.NewGuid();
        var vehicleModelView = new VehicleModelView { Name = "X5", Abrv = "X", MakeId = Guid.NewGuid() };
        var vehicleModel = new VehicleModel { Id = modelId, Name = "X5", Abrv = "X" };
        _mapperMock.Setup(mapper => mapper.Map<VehicleModel>(vehicleModelView))
            .Returns(vehicleModel);
        _vehicleModelServiceMock.Setup(service => service.UpdateVehicleModelAsync(vehicleModel))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.UpdateAsync(modelId, vehicleModelView);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result);  // Expect OkObjectResult
        actionResult.Value.Should().Be("Updated");
    }


    [Fact]
    public async Task DeleteAsync_ShouldReturnOkResult_WhenVehicleModelDeleted()
    {
        // Arrange
        var modelId = Guid.NewGuid();
        _vehicleModelServiceMock.Setup(service => service.DeleteVehicleModelAsync(modelId))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteAsync(modelId);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result);  // Expect OkObjectResult
        actionResult.Value.Should().Be("Deleted");
    }



}
