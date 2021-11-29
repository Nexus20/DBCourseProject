using CourseProject.WEB;

namespace CourseProject.DAL {
    public static class WebLayerExtensions {

        public static IServiceCollection AddWebLayer(this IServiceCollection services) {

            services.AddAutoMapper(typeof(AutomapperWebProfile));

            return services;
        }

    }
}