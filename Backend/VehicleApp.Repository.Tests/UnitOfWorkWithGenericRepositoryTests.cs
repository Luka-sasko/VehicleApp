using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using VehicleApp.Common;
using VehicleApp.DAL;
using VehicleApp.Model;
using VehicleApp.Repository.Common;

namespace VehicleApp.Repository.Tests
{
    public class UnitOfWorkWithGenericRepositoryTests
    {
        private readonly DbContextOptions<VehicleContext> _dbOptions;

        public UnitOfWorkWithGenericRepositoryTests()
        {
            _dbOptions = new DbContextOptionsBuilder<VehicleContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }




        [Fact]
        public async Task GetByIdAsync_ReturnsVehicleModel_WhenIdExists()
        {
            var modelId = Guid.NewGuid();
            var makeId = Guid.NewGuid();

            using var context = new VehicleContext(_dbOptions);
            context.VehicleMakes.Add(new VehicleMake { Id = makeId, Name = "BMW", Abrv = "B" });
            context.VehicleModels.Add(new VehicleModel { Id = modelId, Name = "X5", Abrv = "X", MakeId = makeId });
            await context.SaveChangesAsync();

            var unitOfWork = new UnitOfWork(context);
            var repository = unitOfWork.GetRepository<VehicleModel>();
            var result = await repository.GetByIdAsync(modelId);

            result.Should().NotBeNull();
            result.Id.Should().Be(modelId);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenModelDoesNotExist()
        {
            using var context = new VehicleContext(_dbOptions);
            var unitOfWork = new UnitOfWork(context);

            var repository = unitOfWork.GetRepository<VehicleModel>();
            var result = await repository.GetByIdAsync(Guid.NewGuid());

            result.Should().BeNull();
        }





        [Fact]
        public async Task GetAllAsync_ReturnsAllVehicleModels_WhenNoFilterIsApplied()
        {
            using var context = new VehicleContext(_dbOptions);
            var makeId = Guid.NewGuid();

            context.VehicleMakes.Add(new VehicleMake { Id = makeId, Name = "BMW", Abrv = "B" });
            context.VehicleModels.AddRange(
                new VehicleModel { Id = Guid.NewGuid(), Name = "X5", Abrv = "X", MakeId = makeId },
                new VehicleModel { Id = Guid.NewGuid(), Name = "X3", Abrv = "X3", MakeId = makeId }
            );
            await context.SaveChangesAsync();

            var unitOfWork = new UnitOfWork(context);
            var repository = unitOfWork.GetRepository<VehicleModel>();

            var paging = new Paging { PageNumber = 1, PageSize = 10 };
            var sorting = new Sorting { SortBy = "Name", SortOrder = "asc" };

            var result = await repository.GetAllAsync(null, paging, sorting);

            result.Should().NotBeNull();
            result.Items.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsFilteredVehicleModels()
        {
            using var context = new VehicleContext(_dbOptions);
            var makeId = Guid.NewGuid();

            context.VehicleMakes.Add(new VehicleMake { Id = makeId, Name = "Audi", Abrv = "A" });
            context.VehicleModels.AddRange(
                new VehicleModel { Id = Guid.NewGuid(), Name = "A4", Abrv = "A4", MakeId = makeId },
                new VehicleModel { Id = Guid.NewGuid(), Name = "Q5", Abrv = "Q5", MakeId = makeId }
            );
            await context.SaveChangesAsync();

            var unitOfWork = new UnitOfWork(context);
            var repository = unitOfWork.GetRepository<VehicleModel>();

            var paging = new Paging { PageNumber = 1, PageSize = 10 };
            var sorting = new Sorting { SortBy = "Name", SortOrder = "asc" };

            var result = await repository.GetAllAsync(m => m.Name.Contains("A"), paging, sorting);

            result.Should().NotBeNull();
            result.Items.Should().ContainSingle(m => m.Name == "A4");
        }






        [Fact]
        public async Task AddAsync_AddsNewVehicleModel()
        {
            using var context = new VehicleContext(_dbOptions);
            var makeId = Guid.NewGuid();

            context.VehicleMakes.Add(new VehicleMake { Id = makeId, Name = "BMW", Abrv = "B" });
            await context.SaveChangesAsync();

            var unitOfWork = new UnitOfWork(context);
            var repository = unitOfWork.GetRepository<VehicleModel>();

            var newModel = new VehicleModel
            {
                Id = Guid.NewGuid(),
                Name = "X1",
                Abrv = "X1",
                MakeId = makeId
            };

            await repository.AddAsync(newModel);
            await unitOfWork.CommitAsync();

            var result = await repository.GetByIdAsync(newModel.Id);
            result.Should().NotBeNull();
            result.Name.Should().Be("X1");
        }

        [Fact]
        public async Task AddAsync_ThrowsArgumentNullException_WhenEntityIsNull()
        {
            using var context = new VehicleContext(_dbOptions);
            var unitOfWork = new UnitOfWork(context);
            var repository = unitOfWork.GetRepository<VehicleModel>();

            Func<Task> act = async () =>
            {
                await repository.AddAsync(null);
                await unitOfWork.CommitAsync();
            };

            await act.Should().ThrowAsync<ArgumentNullException>();
        }





        [Fact]
        public async Task UpdateAsync_UpdatesExistingVehicleModel()
        {
            using var context = new VehicleContext(_dbOptions);
            var makeId = Guid.NewGuid();
            var modelId = Guid.NewGuid();

            context.VehicleMakes.Add(new VehicleMake { Id = makeId, Name = "BMW", Abrv = "B" });
            context.VehicleModels.Add(new VehicleModel { Id = modelId, Name = "X2", Abrv = "X2", MakeId = makeId });
            await context.SaveChangesAsync();

            var unitOfWork = new UnitOfWork(context);
            var repository = unitOfWork.GetRepository<VehicleModel>();

            var existingModel = await repository.GetByIdAsync(modelId);
            existingModel.Name = "X2 Updated";
            existingModel.Abrv = "X2U";

            repository.Update(existingModel);
            await unitOfWork.CommitAsync();

            var result = await repository.GetByIdAsync(modelId);
            result.Name.Should().Be("X2 Updated");
            result.Abrv.Should().Be("X2U");
        }

        [Fact]
        public async Task Update_ThrowsException_WhenEntityDoesNotExist()
        {
            using var context = new VehicleContext(_dbOptions);
            var unitOfWork = new UnitOfWork(context);
            var repository = unitOfWork.GetRepository<VehicleModel>();

            var nonExistentModel = new VehicleModel
            {
                Id = Guid.NewGuid(),
                Name = "Ghost",
                Abrv = "G",
                MakeId = Guid.NewGuid()
            };

            Func<Task> act = async () =>
            {
                repository.Update(nonExistentModel);
                await unitOfWork.CommitAsync();
            };

            await act.Should().ThrowAsync<DbUpdateConcurrencyException>();
        }






        [Fact]
        public async Task DeleteAsync_DeletesVehicleModel()
        {
            using var context = new VehicleContext(_dbOptions);
            var makeId = Guid.NewGuid();
            var modelId = Guid.NewGuid();

            context.VehicleMakes.Add(new VehicleMake { Id = makeId, Name = "BMW", Abrv = "B" });
            context.VehicleModels.Add(new VehicleModel { Id = modelId, Name = "X6", Abrv = "X6", MakeId = makeId });
            await context.SaveChangesAsync();

            var unitOfWork = new UnitOfWork(context);
            var repository = unitOfWork.GetRepository<VehicleModel>();

            var model = await repository.GetByIdAsync(modelId);
            repository.Delete(model);
            await unitOfWork.CommitAsync();

            var result = await repository.GetByIdAsync(modelId);
            result.Should().BeNull();
        }

        [Fact]
        public async Task Delete_ThrowsArgumentNullException_WhenEntityIsNull()
        {
            using var context = new VehicleContext(_dbOptions);
            var unitOfWork = new UnitOfWork(context);
            var repository = unitOfWork.GetRepository<VehicleModel>();

            Func<Task> act = async () =>
            {
                await Task.Run(() => repository.Delete(null));
                await unitOfWork.CommitAsync();
            };

            await act.Should().ThrowAsync<ArgumentNullException>()
                .WithMessage("Entity cannot be null. (Parameter 'entity')");
        }










        [Fact]
        public async Task MultipleRepositories_WorkCorrectlyWithinSameUnitOfWork()
        {
            using var context = new VehicleContext(_dbOptions);
            var unitOfWork = new UnitOfWork(context);
            var makeRepository = unitOfWork.GetRepository<VehicleMake>();
            var modelRepository = unitOfWork.GetRepository<VehicleModel>();

            var makeId = Guid.NewGuid();
            var make = new VehicleMake { Id = makeId, Name = "Mercedes", Abrv = "M" };
            await makeRepository.AddAsync(make);
            await unitOfWork.CommitAsync();

            var model = new VehicleModel { Id = Guid.NewGuid(), Name = "GLE", Abrv = "GLE", MakeId = makeId };
            await modelRepository.AddAsync(model);
            await unitOfWork.CommitAsync();

            var fetchedMake = await makeRepository.GetByIdAsync(makeId);
            var fetchedModel = await modelRepository.GetByIdAsync(model.Id);

            fetchedMake.Should().NotBeNull();
            fetchedModel.Should().NotBeNull();
        }

    }
}
