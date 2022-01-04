using AutoMapper;
using CourseProject.BLL.DTO;
using CourseProject.BLL.DTO.StatisticsDtos;
using CourseProject.BLL.ReportDtos;
using CourseProject.DAL.Entities;
using CourseProject.DAL.ReportModels;
using CourseProject.DAL.StatisticsModels;
using Microsoft.AspNetCore.Identity;
using SupplyOrdersReportDto = CourseProject.BLL.ReportDtos.SupplyOrdersReportDto;

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

        CreateMap<CarInStock, CarInStockDto>()
            .ForMember(dto => dto.EquipmentItemValues,
                options => options.MapFrom(p => p.CarInStockEquipmentItemValues.Select(x => x)))
            .ReverseMap()
            .ForMember(entity => entity.CarInStockEquipmentItemValues,
                options => options.Ignore());

        CreateMap<PurchaseOrderEquipmentItemValue, EquipmentItemValueDto>()
            .ForMember(dto => dto.Id,
                o => o.MapFrom(x => x.EquipmentItemValue.Id))
            .ForMember(dto => dto.Value,
                o => o.MapFrom(x => x.EquipmentItemValue.Value))
            .ForMember(dto => dto.Price,
                o => o.MapFrom(x => x.EquipmentItemValue.Price))
            .ForMember(dto => dto.EquipmentItem,
                o => o.MapFrom(x => x.EquipmentItemValue.EquipmentItem));

        CreateMap<CarInStockEquipmentItemValue, EquipmentItemValueDto>()
            .ForMember(dto => dto.Id,
                o => o.MapFrom(x => x.EquipmentItemValue.Id))
            .ForMember(dto => dto.Value,
                o => o.MapFrom(x => x.EquipmentItemValue.Value))
            .ForMember(dto => dto.Price,
                o => o.MapFrom(x => x.EquipmentItemValue.Price))
            .ForMember(dto => dto.EquipmentItem,
                o => o.MapFrom(x => x.EquipmentItemValue.EquipmentItem));

        CreateMap<SupplyOrderPartEquipmentItemValue, EquipmentItemValueDto>()
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

        CreateMap<SupplyOrder, SupplyOrderDto>().ReverseMap();
        CreateMap<SupplyOrderPart, SupplyOrderPartDto>()
            .ForMember(dto => dto.EquipmentItemsValues,
                options => options.MapFrom(p => p.SupplyOrderPartEquipmentItemsValues.Select(x => x)))
            .ReverseMap()
            .ForMember(entity => entity.SupplyOrderPartEquipmentItemsValues,
                options => options.Ignore());

        CreateMap<MaxOrdersClient, MaxOrdersClientDto>().ReverseMap();
        CreateMap<MostPurchasedModel, MostPurchasedModelDto>().ReverseMap();
        CreateMap<MaxPurchaseOrdersManager, MaxPurchaseOrdersManagerDto>().ReverseMap();
        CreateMap<OrdersProfit, OrdersProfitDto>().ReverseMap();

        CreateMap<PurchaseOrdersReport, PurchaseOrdersReportDto>().ReverseMap();
        CreateMap<PurchaseOrdersReportPart, PurchaseOrdersReportPartDto>().ReverseMap();
        CreateMap<SupplyOrdersReport, SupplyOrdersReportDto>().ReverseMap();
        CreateMap<SupplyOrdersReportPart, SupplyOrdersReportPartDto>().ReverseMap();


        CreateMap<TopManagersWhoHandleMoreOrdersSettings, TopManagersWhoHandleMoreOrdersSettingsDto>().ReverseMap();
    }

}