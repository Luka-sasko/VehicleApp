using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;
using VehicleApp.DAL;
using VehicleApp.Model;

namespace VehicleApp.Repository.Tests
{
    public class UnitOfWorkWithGenericRepositoryTests
    {
        private readonly Mock<VehicleContext> _mockContext;
        private readonly Mock<DbSet<VehicleMake>> _mockDbSet;
        private readonly UnitOfWork _unitOfWork;

        public UnitOfWorkWithGenericRepositoryTests()
        {
            // Kreiraj i konfiguriraj DbContextOptions
            var mockOptions = new Mock<DbContextOptions<VehicleContext>>();
            mockOptions.Setup(o => o.ContextType).Returns(typeof(VehicleContext));
            mockOptions.Setup(o => o.Extensions).Returns(new List<IDbContextOptionsExtension>()); 

            // Kreiraj mock za VehicleContext sa opcijama
            _mockContext = new Mock<VehicleContext>(mockOptions.Object) { CallBase = true };
            _mockDbSet = new Mock<DbSet<VehicleMake>>();
            _mockContext.Setup(c => c.Set<VehicleMake>()).Returns(_mockDbSet.Object);
            _unitOfWork = new UnitOfWork(_mockContext.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsVehicleMake_WhenIdExists()
        {
            // Arrange
            var vehicleMakeId = Guid.Parse("40fac56c-b4bb-46be-a2d2-8d9644fbe883");
            var expectedMake = new VehicleMake
            {
                Id = vehicleMakeId,
                Name = "BMW",
                Abrv = "Bmw",
                Models = new List<VehicleModel>()
            };

            _mockDbSet.Setup(m => m.FindAsync(vehicleMakeId)).ReturnsAsync(expectedMake);

            // Act
            var repository = _unitOfWork.GetRepository<VehicleMake>();
            var result = await repository.GetByIdAsync(vehicleMakeId);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(vehicleMakeId);
            result.Name.Should().Be("BMW");
            result.Abrv.Should().Be("Bmw");
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenIdDoesNotExist()
        {
            // Arrange
            var vehicleMakeId = Guid.NewGuid();
            _mockDbSet.Setup(m => m.FindAsync(vehicleMakeId)).ReturnsAsync((VehicleMake)null);

            // Act
            var repository = _unitOfWork.GetRepository<VehicleMake>();
            var result = await repository.GetByIdAsync(vehicleMakeId);

            // Assert
            result.Should().BeNull();
        }


        
    }
}