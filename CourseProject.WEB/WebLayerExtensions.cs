using CourseProject.WEB.Utils;
using DinkToPdf;
using DinkToPdf.Contracts;

namespace CourseProject.WEB {
    public static class WebLayerExtensions {

        public static IServiceCollection AddWebLayer(this IServiceCollection services) {

            services.AddAutoMapper(typeof(AutomapperWebProfile));
            services.AddSingleton<ITools, PdfTools>();
            services.AddSingleton<IConverter, SynchronizedConverter>();
            services.AddScoped<IViewRenderService, ViewRenderService>();

            return services;
        }

    }
}