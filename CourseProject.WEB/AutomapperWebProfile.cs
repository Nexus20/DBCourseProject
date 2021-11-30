using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.WEB.Models;

namespace CourseProject.WEB; 

public class AutomapperWebProfile : Profile {

    public AutomapperWebProfile() {

        CreateMap<CarDto, CarViewModel>().ReverseMap();
        CreateMap<CarDto, CreateEditCarViewModel>().ReverseMap();
        CreateMap<BrandDto, BrandViewModel>().ReverseMap();
        CreateMap<BrandDto, CreateEditBrandViewModel>().ReverseMap();
        CreateMap<ModelDto, ModelViewModel>().ReverseMap();
        CreateMap<ModelDto, CreateEditModelViewModel>().ReverseMap();
        CreateMap<SupplierDto, SupplierViewModel>().ReverseMap();
        CreateMap<CarPhotoDto, CarPhotoViewModel>().ReverseMap();
        CreateMap<EquipmentItemDto, EquipmentItemViewModel>().ReverseMap();
        CreateMap<EquipmentItemValueDto, EquipmentItemValueViewModel>().ReverseMap();
        CreateMap<EquipmentItemCategoryDto, EquipmentItemCategoryViewModel>().ReverseMap();
        CreateMap<EquipmentItemCategoryDto, CreateEditEquipmentItemCategoryViewModel>().ReverseMap();
    }

}