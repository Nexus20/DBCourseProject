using CourseProject.BLL.Interfaces;
using CourseProject.BLL.Services;
using CourseProject.DAL;
using Microsoft.Extensions.DependencyInjection;

namespace CourseProject.BLL {
    public static class BusinessLogicLayerExtensions {

        public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services, string connectionString) {
            services.AddDataAccessLayer(connectionString);

            services.AddAutoMapper(typeof(AutomapperBllProfile));

            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISignInService, SignInService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IModelService, ModelService>();
            services.AddScoped<IEquipmentItemCategoryService, EquipmentItemCategoryService>();
            services.AddScoped<IEquipmentItemService, EquipmentItemService>();
            services.AddScoped<IEquipmentItemValueService, EquipmentItemValueService>();

            return services;
        }
    }
}