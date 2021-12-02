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
        CreateMap<CarPhoto, CarPhotoDto>().ReverseMap();
        CreateMap<EquipmentItem, EquipmentItemDto> ().ReverseMap();
        CreateMap<EquipmentItemValue, EquipmentItemValueDto> ().ReverseMap();
        CreateMap<EquipmentItemCategory, EquipmentItemCategoryDto> ().ReverseMap();
        CreateMap<User, UserDto> ().ReverseMap();
    }

}