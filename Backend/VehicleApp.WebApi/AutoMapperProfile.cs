namespace VehicleApp.WebApi
{
    using AutoMapper;
    using VehicleApp.Common;
    using VehicleApp.Model;

    public class AutoMapperProfile : Profile
        {
            public AutoMapperProfile()
            {
                CreateMap<VehicleMake,VehicleMakeView >().ReverseMap();
                CreateMap<PagedList<VehicleMake>, PagedList<VehicleMakeView>>()
                    .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                    .ForMember(dest => dest.PageNumber, opt => opt.MapFrom(src => src.PageNumber))
                    .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize))
                    .ForMember(dest => dest.TotalCount, opt => opt.MapFrom(src => src.TotalCount))
                    .ForMember(dest => dest.TotalPages, opt => opt.MapFrom(src => src.TotalPages));

                CreateMap<VehicleModel, VehicleModelView>().ReverseMap();
                CreateMap<PagedList<VehicleModel>, PagedList<VehicleModelView>>()
                    .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                    .ForMember(dest => dest.PageNumber, opt => opt.MapFrom(src => src.PageNumber))
                    .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize))
                    .ForMember(dest => dest.TotalCount, opt => opt.MapFrom(src => src.TotalCount))
                    .ForMember(dest => dest.TotalPages, opt => opt.MapFrom(src => src.TotalPages));


        }
    }
    

}
