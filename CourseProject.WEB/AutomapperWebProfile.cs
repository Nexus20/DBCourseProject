using AutoMapper;
using CourseProject.WEB.Models;

namespace CourseProject.WEB; 

public class AutomapperWebProfile : Profile {

    public AutomapperWebProfile() {

        CreateMap<CarViewModel, CarViewModel>().ReverseMap();
        CreateMap<BrandViewModel, BrandViewModel>().ReverseMap();
        CreateMap<ModelViewModel, ModelViewModel>().ReverseMap();
        CreateMap<SupplierViewModel, SupplierViewModel>().ReverseMap();
    }

}