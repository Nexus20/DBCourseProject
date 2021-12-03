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
        CreateMap<EquipmentItem, EquipmentItemDto>().ReverseMap();
        CreateMap<EquipmentItemValue, EquipmentItemValueDto>().ReverseMap();
        CreateMap<EquipmentItemCategory, EquipmentItemCategoryDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<Client, UserDto>().ReverseMap();
        CreateMap<Client, ClientDto>().ReverseMap();
        CreateMap<Manager, ManagerDto>().ReverseMap();
        CreateMap<Showroom, ShowroomDto>().ReverseMap();
        CreateMap<IdentityRole, RoleDto>().ReverseMap();

        CreateMap<PurchaseOrderEquipmentItemValue, EquipmentItemValueDto>()
            .ForMember(dto => dto.Id,
                o => o.MapFrom(x => x.EquipmentItemValue.Id))
            .ForMember(dto => dto.Value,
                o => o.MapFrom(x => x.EquipmentItemValue.Value))
            .ForMember(dto => dto.Price,
                o => o.MapFrom(x => x.EquipmentItemValue.Price))
            .ForMember(dto => dto.EquipmentItem,
                o => o.MapFrom(x => x.EquipmentItemValue.EquipmentItem));

        CreateMap<PurchaseOrder, PurchaseOrderDto>()
            .ForMember(dto => dto.EquipmentItemsValues,
                options => options.MapFrom(p => p.PurchaseOrderEquipmentItemsValues.Select(x => x)))
            .ReverseMap()
            .ForMember(entity => entity.PurchaseOrderEquipmentItemsValues,
                options => options.Ignore());
    }

}