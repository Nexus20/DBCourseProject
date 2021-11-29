using CourseProject.DAL.Interfaces;
using CourseProject.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CourseProject.DAL {
    public static class DataAccessLayerExtensions {

        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, string connectionString) {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            return services;
        }

    }
}