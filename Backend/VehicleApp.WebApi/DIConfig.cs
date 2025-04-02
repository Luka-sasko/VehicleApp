namespace VehicleApp.WebApi
{
    using Autofac;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using VehicleApp.DAL;
    using VehicleApp.Repository;
    using VehicleApp.Repository.Common;
    using VehicleApp.Service;
    using VehicleApp.Service.Common;

    public class DIConfig : Module
    {
        private readonly IConfiguration _configuration;

        public DIConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void Load(ContainerBuilder builder)
        {
            // Registracija DbContaxa
            builder.Register(context =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<VehicleContext>();
                optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DBConnection"));
                return new VehicleContext(optionsBuilder.Options);
            }).As<VehicleContext>().InstancePerLifetimeScope();

            // Registracija repozitorija
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<VehicleMakeRepository>().As<IVehicleMakeRepository>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();

            // Registracija servisa
            builder.RegisterType<VehicleMakeService>().As<IVehicleMakeService>();

            // Registracija AutoMapper-a
            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            })).AsSelf().SingleInstance();

            builder.Register(context =>
            {
                var scope = context.Resolve<ILifetimeScope>();
                return new Mapper(scope.Resolve<MapperConfiguration>(), scope.Resolve);
            }).As<IMapper>().InstancePerLifetimeScope();
        }
    }

}
