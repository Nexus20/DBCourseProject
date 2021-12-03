using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.DAL.Entities;
using Microsoft.AspNetCore.Identity;

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
        CreateMap<Client, UserDto> ().ReverseMap();
        CreateMap<Client, ClientDto> ().ReverseMap();
        CreateMap<Manager, ManagerDto> ().ReverseMap();
        CreateMap<Showroom, ShowroomDto> ().ReverseMap();
        CreateMap<IdentityRole, RoleDto> ().ReverseMap();
    }

}