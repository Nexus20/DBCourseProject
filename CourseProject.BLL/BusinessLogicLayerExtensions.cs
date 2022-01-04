using CourseProject.BLL.DataHandlers;
using CourseProject.BLL.DataHandlers.BrandDataHandlers;
using CourseProject.BLL.DataHandlers.CarDataHandlers;
using CourseProject.BLL.DataHandlers.CarInStockDataHandlers;
using CourseProject.BLL.DataHandlers.EquipmentItemCategoryDataHandlers;
using CourseProject.BLL.DataHandlers.ModelDataHandlers;
using CourseProject.BLL.DataHandlers.PurchaseOrderDataHandlers;
using CourseProject.BLL.DataHandlers.ShowroomDataHandlers;
using CourseProject.BLL.DataHandlers.SupplierDataHandlers;
using CourseProject.BLL.DataHandlers.SupplyOrderDataHandlers;
using CourseProject.BLL.FilterModels;
using CourseProject.BLL.Interfaces;
using CourseProject.BLL.PipelineBuilders;
using CourseProject.BLL.Services;
using CourseProject.DAL;
using CourseProject.DAL.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace CourseProject.BLL {
    public static class BusinessLogicLayerExtensions {

        public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services, string connectionString) {
            services.AddDataAccessLayer(connectionString);

            services.AddAutoMapper(typeof(AutomapperBllProfile));

            services.AddScoped<ICarService, CarService>();
            services.AddScoped<ICarInStockService, CarInStockService>();
            services.AddScoped<IShowroomService, ShowroomService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IManagerService, ManagerService>();
            services.AddScoped<ISignInService, SignInService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IModelService, ModelService>();
            services.AddScoped<IEquipmentItemCategoryService, EquipmentItemCategoryService>();
            services.AddScoped<IEquipmentItemService, EquipmentItemService>();
            services.AddScoped<IEquipmentItemValueService, EquipmentItemValueService>();
            services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
            services.AddScoped<ISupplyOrderService, SupplyOrderService>();
            services.AddScoped<IStatisticsService, StatisticsService>();

            services.AddScoped(typeof(IPipelineBuilder<,>), typeof(SelectionPipelineBuilder<,>));

            services.AddScoped<IPipelineBuilderDirector<Car, CarFilterModel>, CarSelectionPipelineBuilderDirector>();

            services.AddScoped<CarBrandModelFilterDataHandler>();
            services.AddScoped<CarOrderDataHandler>();
            services.AddScoped<CarModelSearchDataHandler>();

            services.AddScoped<IPipelineBuilderDirector<CarInStock, CarInStockFilterModel>, CarInStockSelectionPipelineBuilderDirector>();

            services.AddScoped<CarInStockBrandModelFilterDataHandler>();
            services.AddScoped<CarInStockShowroomFilterDataHandler>();
            services.AddScoped<CarInStockOrderDataHandler>();
            services.AddScoped<CarInStockModelSearchDataHandler>();

            services.AddScoped<IPipelineBuilderDirector<Model, ModelFilterModel>, ModelSelectionPipelineBuilderDirector>();

            services.AddScoped<ModelBrandFilterDataHandler>();
            services.AddScoped<ModelOrderDataHandler>();
            services.AddScoped<ModelSearchDataHandler>();

            services.AddScoped<IPipelineBuilderDirector<Brand, BrandFilterModel>, BrandSelectionPipelineBuilderDirector>();

            services.AddScoped<BrandSearchDataHandler>();
            services.AddScoped<BrandOrderDataHandler>();

            services.AddScoped<IPipelineBuilderDirector<Supplier, SupplierFilterModel>, SupplierSelectionPipelineBuilderDirector>();

            services.AddScoped<SupplierBrandFilterDataHandler>();
            services.AddScoped<SupplierOrderDataHandler>();
            services.AddScoped<SupplierNameSearchDataHandler>();
            services.AddScoped<SupplierEmailSearchDataHandler>();
            services.AddScoped<SupplierPhoneSearchDataHandler>();

            services.AddScoped<IPipelineBuilderDirector<PurchaseOrder, PurchaseOrderFilterModel>, PurchaseOrderSelectionPipelineBuilderDirector>();

            services.AddScoped<PurchaseOrderClientEmailSearchDataHandler>();
            services.AddScoped<PurchaseOrderClientPhoneSearchDataHandler>();
            services.AddScoped<PurchaseOrderCreationDateFilterDataHandler>();
            services.AddScoped<PurchaseOrderLastUpdateDateFilterDataHandler>();
            services.AddScoped<PurchaseOrderManagerFilterDataHandler>();
            services.AddScoped<PurchaseOrderOrderDataHandler>();
            services.AddScoped<PurchaseOrderOrderIdFilterDataHandler>();
            services.AddScoped<PurchaseOrderStateFilterDataHandler>();

            services.AddScoped<IPipelineBuilderDirector<SupplyOrder, SupplyOrderFilterModel>, SupplyOrderSelectionPipelineBuilderDirector>();

            services.AddScoped<SupplyOrderSupplierEmailSearchDataHandler>();
            services.AddScoped<SupplyOrderSupplierPhoneSearchDataHandler>();
            services.AddScoped<SupplyOrderSupplierNameSearchDataHandler>();
            services.AddScoped<SupplyOrderCreationDateFilterDataHandler>();
            services.AddScoped<SupplyOrderLastUpdateDateFilterDataHandler>();
            services.AddScoped<SupplyOrderManagerFilterDataHandler>();
            services.AddScoped<SupplyOrderOrderDataHandler>();
            services.AddScoped<SupplyOrderOrderIdFilterDataHandler>();
            services.AddScoped<SupplyOrderSupplierIdFilterDataHandler>();
            services.AddScoped<SupplyOrderStateFilterDataHandler>();

            services.AddScoped<IPipelineBuilderDirector<Showroom, ShowroomFilterModel>, ShowroomSelectionPipelineBuilderDirector>();

            services.AddScoped<ShowroomAddressSearchDataHandler>();
            services.AddScoped<ShowroomPhoneSearchDataHandler>();
            services.AddScoped<ShowroomOrderDataHandler>();

            services.AddScoped<IPipelineBuilderDirector<EquipmentItemCategory, EquipmentItemCategoryFilterModel>, EquipmentItemCategorySelectionPipelineBuilderDirector>();

            services.AddScoped<EquipmentItemCategoryNameDataHandler>();
            services.AddScoped<EquipmentItemCategoryUnitsOfMeasureSearchDataHandler>();
            services.AddScoped<EquipmentItemCategoryOrderDataHandler>();

            services.AddScoped(typeof(SkipObjectsDataHandler<,>));
            services.AddScoped(typeof(TakeObjectsDataHandler<,>));

            return services;
        }
    }
}