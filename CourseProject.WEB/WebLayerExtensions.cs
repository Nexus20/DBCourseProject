namespace CourseProject.WEB {
    public static class WebLayerExtensions {

        public static IServiceCollection AddWebLayer(this IServiceCollection services) {

            services.AddAutoMapper(typeof(AutomapperWebProfile));

            return services;
        }

    }
}