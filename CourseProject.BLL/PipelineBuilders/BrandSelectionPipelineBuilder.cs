using CourseProject.BLL.DataHandlers;
using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace CourseProject.BLL.PipelineBuilders {
    public class BrandSelectionPipelineBuilder : IPipelineBuilder<Brand, BrandFilterModel> {

        private readonly IServiceProvider _serviceProvider;

        private IDataHandler<Brand, BrandFilterModel> _firstDataHandler;

        private IDataHandler<Brand, BrandFilterModel> _currentDataHandler;

        public BrandSelectionPipelineBuilder(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public IPipelineBuilder<Brand, BrandFilterModel> SetFirstChainPart<T>() where T : IDataHandler<Brand, BrandFilterModel> {

            var dataHandler = _serviceProvider.GetRequiredService<T>();
            _firstDataHandler = _currentDataHandler = dataHandler;
            return this;
        }

        public IPipelineBuilder<Brand, BrandFilterModel> SetNextChainPart<T>() where T : IDataHandler<Brand, BrandFilterModel> {

            var dataHandler = _serviceProvider.GetRequiredService<T>();
            _currentDataHandler = _currentDataHandler.SetNext(dataHandler);
            return this;
        }

        public IDataHandler<Brand, BrandFilterModel> GetPipeline() {
            return _firstDataHandler;
        }
    }
}