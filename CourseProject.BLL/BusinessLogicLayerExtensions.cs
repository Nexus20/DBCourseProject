using CourseProject.BLL.DataHandlers;
using CourseProject.BLL.DataHandlers.BrandDataHandlers;
using CourseProject.BLL.DataHandlers.CarDataHandlers;
using CourseProject.BLL.DataHandlers.ModelDataHandlers;
using CourseProject.BLL.DataHandlers.PurchaseOrderDataHandlers;
using CourseProject.BLL.DataHandlers.SupplierDataHandlers;
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
            services.AddScoped<IStatisticsService, StatisticsService>();

            services.AddScoped(typeof(IPipelineBuilder<,>), typeof(SelectionPipelineBuilder<,>));

            services.AddScoped<IPipelineBuilderDirector<Car, CarFilterModel>, CarSelectionPipelineBuilderDirector>();

            services.AddScoped<CarBrandModelFilterDataHandler>();
            services.AddScoped<CarOrderDataHandler>();
            services.AddScoped<CarModelSearchDataHandler>();

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

            services.AddScoped(typeof(SkipObjectsDataHandler<,>));
            services.AddScoped(typeof(TakeObjectsDataHandler<,>));

            return services;
        }
    }
}