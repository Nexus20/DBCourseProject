using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.DAL.Entities;

namespace CourseProject.BLL; 

public class AutomapperBllProfile : Profile {

    public AutomapperBllProfile() {

        CreateMap<Car, CarDto>().ReverseMap();
        CreateMap<Brand, BrandDto>().ReverseMap();
        CreateMap<Model, ModelDto>().ReverseMap();
        CreateMap<Supplier, SupplierDto>().ReverseMap();
    }

}