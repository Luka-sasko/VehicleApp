namespace VehicleApp.WebApi
{
    using Autofac;
    using AutoMapper;
    using VehicleApp.Repository;
    using VehicleApp.Repository.Common;
    using VehicleApp.Service;
    using VehicleApp.Service.Common;

    public class DIConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Registracija repozitorija
            builder.RegisterType<VehicleMakeRepository>().As<IVehicleMakeRepository>();

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
