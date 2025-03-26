namespace VehicleApp.WebApi
{
    using AutoMapper;
    using VehicleApp.Model;

    public class AutoMapperProfile : Profile
        {
            public AutoMapperProfile()
            {
                CreateMap<VehicleMake,VehicleMakeView >().ReverseMap();
                //CreateMap<VehicleModel, >().ReverseMap();
            }
        }
    

}
