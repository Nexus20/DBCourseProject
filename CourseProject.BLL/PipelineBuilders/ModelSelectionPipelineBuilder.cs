using CourseProject.BLL.DataHandlers;
using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace CourseProject.BLL.PipelineBuilders {
    public class ModelSelectionPipelineBuilder : IPipelineBuilder<Model, ModelFilterModel> {

        private readonly IServiceProvider _serviceProvider;

        private IDataHandler<Model, ModelFilterModel> _firstDataHandler;

        private IDataHandler<Model, ModelFilterModel> _currentDataHandler;

        public ModelSelectionPipelineBuilder(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public IPipelineBuilder<Model, ModelFilterModel> SetFirstChainPart<T>() where T : IDataHandler<Model, ModelFilterModel> {

            var dataHandler = _serviceProvider.GetRequiredService<T>();
            _firstDataHandler = _currentDataHandler = dataHandler;
            return this;
        }

        public IPipelineBuilder<Model, ModelFilterModel> SetNextChainPart<T>() where T : IDataHandler<Model, ModelFilterModel> {

            var dataHandler = _serviceProvider.GetRequiredService<T>();
            _currentDataHandler = _currentDataHandler.SetNext(dataHandler);
            return this;
        }

        public IDataHandler<Model, ModelFilterModel> GetPipeline() {
            return _firstDataHandler;
        }
    }
}