using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CourseProject.DAL {
    public static class DataAccessLayerExtensions {

        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, string connectionString) {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            return services;
        }

    }
}