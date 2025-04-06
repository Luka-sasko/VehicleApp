using Moq;
using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VehicleApp.Model;
using VehicleApp.Service;
using VehicleApp.Repository.Common;
using VehicleApp.Common;
using VehicleApp.Service.Common;
using Xunit;
using VehicleApp.DAL;
using VehicleApp.Repository;

namespace VehicleApp.Tests
{
    public class VehicleModelServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IGenericRepository<VehicleModel>> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly IVehicleModelService _service;

        public VehicleModelServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _repositoryMock = new Mock<IGenericRepository<VehicleModel>>();
            _mapperMock = new Mock<IMapper>();

            _unitOfWorkMock.Setup(uow => uow.GetRepository<VehicleModel>()).Returns(_repositoryMock.Object);
            _service = new VehicleModelService(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetVehicleModelByIdAsync_ShouldReturnVehicleModel_WhenIdExists()
        {
            var modelId = Guid.NewGuid();
            var vehicleModel = new VehicleModel { Id = modelId, Name = "X5", Abrv = "X" };

            _repositoryMock.Setup(repo => repo.GetByIdAsync(modelId)).ReturnsAsync(vehicleModel);

            _mapperMock.Setup(m => m.Map<VehicleModelView>(vehicleModel)).Returns(new VehicleModelView { Id = modelId, Name = "X5" });

            // Act
            var result = await _service.GetVehicleModelByIdAsync(modelId);

            // Assert
            result.Should().NotBeNull();  
            result.Id.Should().Be(modelId); 
            result.Name.Should().Be("X5");  
            _repositoryMock.Verify(repo => repo.GetByIdAsync(modelId), Times.Once);  
        }


        [Fact]
        public async Task GetVehicleModelByIdAsync_ShouldReturnNull_WhenModelDoesNotExist()
        {
            // Arrange
            var modelId = Guid.NewGuid();
            _repositoryMock.Setup(repo => repo.GetByIdAsync(modelId)).ReturnsAsync((VehicleModel)null);

            // Act
            var result = await _service.GetVehicleModelByIdAsync(modelId);

            // Assert
            result.Should().BeNull();
            _repositoryMock.Verify(repo => repo.GetByIdAsync(modelId), Times.Once);
        }

        [Fact]
            public async Task GetAllAsync_ShouldReturnPagedVehicleModels()
        {
            // Arrange
            var predicate = (Expression<Func<VehicleModel, bool>>)(vm => vm.Name.Contains("X"));
            var paging = new Paging { PageNumber = 1, PageSize = 10 };
            var sorting = new Sorting { SortBy = "Name", SortOrder = "asc" };

            var vehicleModels = new List<VehicleModel>
            {
                new VehicleModel { Id = Guid.NewGuid(), Name = "X5", Abrv = "X" },
                new VehicleModel { Id = Guid.NewGuid(), Name = "X3", Abrv = "X3" }
            };

            var pagedList = new PagedList<VehicleModel>(vehicleModels, 1, 10, vehicleModels.Count);
            _repositoryMock.Setup(repo => repo.GetAllAsync(predicate, paging, sorting)).ReturnsAsync(pagedList);

            var vehicleModelViews = new List<VehicleModelView>
            {
                new VehicleModelView { Id = vehicleModels[0].Id, Name = vehicleModels[0].Name },
                new VehicleModelView { Id = vehicleModels[1].Id, Name = vehicleModels[1].Name }
            };
            _mapperMock.Setup(m => m.Map<PagedList<VehicleModelView>>(pagedList)).Returns(new PagedList<VehicleModelView>(vehicleModelViews, 1, 10, vehicleModelViews.Count));

            // Act
            var result = await _service.GetAllAsync(predicate, paging, sorting);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(2);
            _repositoryMock.Verify(repo => repo.GetAllAsync(predicate, paging, sorting), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoVehicleModelsExist()
        {
            // Arrange
            var predicate = (Expression<Func<VehicleModel, bool>>)(vm => vm.Name.Contains("X"));
            var paging = new Paging { PageNumber = 1, PageSize = 10 };
            var sorting = new Sorting { SortBy = "Name", SortOrder = "asc" };

            var vehicleModels = new List<VehicleModel>();
            var pagedList = new PagedList<VehicleModel>(vehicleModels, 1, 10, 0);
            _repositoryMock.Setup(repo => repo.GetAllAsync(predicate, paging, sorting)).ReturnsAsync(pagedList);

            var vehicleModelViews = new List<VehicleModelView>();
            _mapperMock.Setup(m => m.Map<PagedList<VehicleModelView>>(pagedList)).Returns(new PagedList<VehicleModelView>(vehicleModelViews, 1, 10, 0));

            // Act
            var result = await _service.GetAllAsync(predicate, paging, sorting);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().BeEmpty();
            _repositoryMock.Verify(repo => repo.GetAllAsync(predicate, paging, sorting), Times.Once);
        }


 

        [Fact]
        public async Task DeleteVehicleModelAsync_ShouldDeleteVehicleModel_WhenModelExists()
        {
            // Arrange
            var modelId = Guid.NewGuid();
            var vehicleModel = new VehicleModel { Id = modelId, Name = "X5", Abrv = "X" };

            _repositoryMock.Setup(repo => repo.GetByIdAsync(modelId)).ReturnsAsync(vehicleModel);

            // Act
            await _service.DeleteVehicleModelAsync(modelId);

            // Assert
            _repositoryMock.Verify(repo => repo.Delete(It.IsAny<VehicleModel>()), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteVehicleModelAsync_ShouldNotDelete_WhenModelDoesNotExist()
        {
            // Arrange
            var modelId = Guid.NewGuid();
            _repositoryMock.Setup(repo => repo.GetByIdAsync(modelId)).ReturnsAsync((VehicleModel)null);

            // Act
            await _service.DeleteVehicleModelAsync(modelId);

            // Assert
            _repositoryMock.Verify(repo => repo.Delete(It.IsAny<VehicleModel>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Never);
        }

        

        [Fact]
        public async Task AddVehicleModelAsync_ShouldThrowException_WhenModelIsNull()
        {
            // Arrange
            VehicleModel vehicleModel = null;

            // Act
            Func<Task> act = async () => await _service.AddVehicleModelAsync(vehicleModel);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task AddVehicleModelAsync_ShouldAddVehicleModel()
        {
            // Arrange
            var vehicleModel = new VehicleModel { Id = Guid.NewGuid(), Name = "X5", Abrv = "X" };

            // Act
            await _service.AddVehicleModelAsync(vehicleModel);

            // Assert
            _repositoryMock.Verify(repo => repo.AddAsync(It.IsAny<VehicleModel>()), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
        }




        [Fact]
        public async Task UpdateVehicleModelAsync_ShouldThrowException_WhenModelIsNull()
        {
            // Arrange
            VehicleModel vehicleModel = null;

            // Act
            Func<Task> act = async () => await _service.UpdateVehicleModelAsync(vehicleModel);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task UpdateVehicleModelAsync_ShouldUpdateVehicleModel()
        {
            // Arrange
            var vehicleModel = new VehicleModel { Id = Guid.NewGuid(), Name = "X5", Abrv = "X" };

            // Act
            await _service.UpdateVehicleModelAsync(vehicleModel);

            // Assert
            _repositoryMock.Verify(repo => repo.Update(It.IsAny<VehicleModel>()), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
        }



        

        
    }
}
