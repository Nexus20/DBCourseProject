using CourseProject.BLL.DataHandlers;
using CourseProject.BLL.FilterModels;
using CourseProject.BLL.Interfaces;
using CourseProject.BLL.PipelineBuilders;
using CourseProject.BLL.Services;
using CourseProject.DAL;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;
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

            services.AddScoped<IPipelineBuilder<SelectionPipelineExpressions<Car>, CarFilterModel>, CarSelectionPipelineBuilder>();
            services.AddScoped<IPipelineBuilderDirector<SelectionPipelineExpressions<Car>, CarFilterModel>, CarSelectionPipelineBuilderDirector>();

            services.AddScoped<BrandModelFilterDataHandler>();
            services.AddScoped<AlphabetOrderDataHandler>();
            services.AddScoped<ModelSearchDataHandler>();
            services.AddScoped<SkipGamesDataHandler>();
            services.AddScoped<TakeGamesDataHandler>();

            return services;
        }
    }
}