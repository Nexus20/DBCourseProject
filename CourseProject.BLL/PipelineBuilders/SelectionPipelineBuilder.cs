using CourseProject.BLL.DataHandlers;
using CourseProject.BLL.FilterModels;
using Microsoft.Extensions.DependencyInjection;

namespace CourseProject.BLL.PipelineBuilders {
    public class SelectionPipelineBuilder<TEntity, TFilterModel> : IPipelineBuilder<TEntity, TFilterModel> where TEntity : class where TFilterModel : FilterModel {

        private readonly IServiceProvider _serviceProvider;

        private IDataHandler<TEntity, TFilterModel> _firstDataHandler;

        private IDataHandler<TEntity, TFilterModel> _currentDataHandler;

        public SelectionPipelineBuilder(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public IPipelineBuilder<TEntity, TFilterModel> SetFirstChainPart<T>() where T : IDataHandler<TEntity, TFilterModel> {

            var dataHandler = _serviceProvider.GetRequiredService<T>();
            _firstDataHandler = _currentDataHandler = dataHandler;
            return this;
        }

        public IPipelineBuilder<TEntity, TFilterModel> SetNextChainPart<T>() where T : IDataHandler<TEntity, TFilterModel> {

            var dataHandler = _serviceProvider.GetRequiredService<T>();
            _currentDataHandler = _currentDataHandler.SetNext(dataHandler);
            return this;
        }

        public IDataHandler<TEntity, TFilterModel> GetPipeline() {
            return _firstDataHandler;
        }
    }
}