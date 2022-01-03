using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.FilterModels;
using CourseProject.WEB.Models;
using CourseProject.WEB.Models.FilterViewModels;

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
        CreateMap<SupplierDto, CreateEditSupplierViewModel>().ReverseMap();
        CreateMap<CarPhotoDto, CarPhotoViewModel>().ReverseMap();
        CreateMap<EquipmentItemDto, EquipmentItemViewModel>().ReverseMap();
        CreateMap<EquipmentItemValueDto, EquipmentItemValueViewModel>().ReverseMap();
        CreateMap<EquipmentItemCategoryDto, EquipmentItemCategoryViewModel>().ReverseMap();
        CreateMap<EquipmentItemCategoryDto, CreateEditEquipmentItemCategoryViewModel>().ReverseMap();
        CreateMap<ShowroomDto, ShowroomViewModel>().ReverseMap();
        CreateMap<ShowroomDto, CreateEditShowroomViewModel>().ReverseMap();
        CreateMap<UserDto, UserViewModel>().ReverseMap();
        CreateMap<ClientDto, ClientViewModel>().ReverseMap();
        CreateMap<ManagerDto, ManagerViewModel>().ReverseMap();
        CreateMap<SupplyOrderDto, SupplyOrderViewModel>().ReverseMap();
        CreateMap<SupplyOrderPartDto, SupplyOrderPartViewModel>().ReverseMap();
        CreateMap<CarInStockDto, CarInStockViewModel>().ReverseMap();

        CreateMap<RegisterViewModel, UserDto>().ReverseMap();
        CreateMap<CreateUserViewModel, UserDto>().ReverseMap();
        CreateMap<EditUserViewModel, UserDto>().ReverseMap();
        CreateMap<ChangePasswordViewModel, UserDto>().ReverseMap();

        CreateMap<RoleDto, RoleViewModel>().ReverseMap();

        CreateMap<PurchaseOrderDto, PurchaseOrderViewModel>();

        CreateMap<CarFilterModel, CarFilterViewModel>().ReverseMap();
        CreateMap<ModelFilterModel, ModelFilterViewModel>().ReverseMap();
        CreateMap<BrandFilterModel, BrandFilterViewModel>().ReverseMap();
        CreateMap<SupplierFilterModel, SupplierFilterViewModel>().ReverseMap();
        CreateMap<PurchaseOrderFilterModel, PurchaseOrderFilterViewModel>().ReverseMap();
        CreateMap<SupplyOrderFilterModel, SupplyOrderFilterViewModel>().ReverseMap();
        CreateMap<CarInStockFilterModel, CarInStockFilterViewModel>().ReverseMap();

        CreateMap<MaxOrdersClientDto, MaxOrdersClientViewModel>().ReverseMap();
        CreateMap<MostPurchasedModelDto, MostPurchasedModelViewModel>().ReverseMap();
        CreateMap<MaxPurchaseOrdersManagerDto, MaxPurchaseOrdersManagerViewModel>().ReverseMap();
    }

}