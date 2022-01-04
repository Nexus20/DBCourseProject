using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.DTO.StatisticsDtos;
using CourseProject.BLL.FilterModels;
using CourseProject.BLL.ReportDtos;
using CourseProject.WEB.Models;
using CourseProject.WEB.Models.FilterViewModels;
using CourseProject.WEB.Models.ReportViewModels;
using CourseProject.WEB.Models.StatisticsViewModels;

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
        CreateMap<UserDto, EditPersonalInfoViewModel>().ReverseMap();
        CreateMap<ManagerDto, ManagerViewModel>().ReverseMap();
        CreateMap<SupplyOrderDto, SupplyOrderViewModel>().ReverseMap();
        CreateMap<SupplyOrderPartDto, SupplyOrderPartViewModel>().ReverseMap();
        CreateMap<CarInStockDto, CarInStockViewModel>().ReverseMap();
        CreateMap<ClientPersonalDataViewModel, ClientPersonalDataDto>().ReverseMap();

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
        CreateMap<ShowroomFilterModel, ShowroomFilterViewModel>().ReverseMap();
        CreateMap<EquipmentItemCategoryFilterModel, EquipmentItemCategoryFilterViewModel>().ReverseMap();

        CreateMap<MaxOrdersClientDto, MaxOrdersClientViewModel>().ReverseMap();
        CreateMap<MostPurchasedModelDto, MostPurchasedModelViewModel>().ReverseMap();
        CreateMap<MaxPurchaseOrdersManagerDto, MaxPurchaseOrdersManagerViewModel>().ReverseMap();
        CreateMap<OrdersProfitDto, OrdersProfitViewModel>().ReverseMap();

        CreateMap<PurchaseOrdersReportDto, PurchaseOrdersReportViewModel>().ReverseMap();
        CreateMap<PurchaseOrdersReportPartDto, PurchaseOrdersReportPartViewModel>().ReverseMap();
        CreateMap<SupplyOrdersReportDto, SupplyOrdersReportViewModel>().ReverseMap();
        CreateMap<SupplyOrdersReportPartDto, SupplyOrdersReportPartViewModel>().ReverseMap();

        CreateMap<TopManagersWhoHandleMoreOrdersSettingsDto, TopManagersWhoHandleMoreOrdersSettingsViewModel>().ReverseMap();
    }

}