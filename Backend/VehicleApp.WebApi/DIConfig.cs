namespace VehicleApp.WebApi
{
    using Autofac;
    using AutoMapper;


    public class DIConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Registracija repozitorija
            //builder.RegisterType<>().As<>();

            // Registracija servisa
            //builder.RegisterType<>().As<>();

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
