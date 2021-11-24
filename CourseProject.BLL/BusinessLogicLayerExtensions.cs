using CourseProject.DAL;
using Microsoft.Extensions.DependencyInjection;

namespace CourseProject.BLL {
    public static class BusinessLogicLayerExtensions {

        public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services, string connectionString) {
            services.AddDataAccessLayer(connectionString);
            return services;
        }
    }
}