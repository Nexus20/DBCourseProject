using CourseProject.BLL.DataHandlers;
using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;
using Microsoft.Extensions.DependencyInjection;

namespace CourseProject.BLL.PipelineBuilders {
    public class CarSelectionPipelineBuilder : IPipelineBuilder<SelectionPipelineExpressions<Car>, CarFilterModel> {

        private readonly IServiceProvider _serviceProvider;

        private IDataHandler<SelectionPipelineExpressions<Car>, CarFilterModel> _firstDataHandler;

        private IDataHandler<SelectionPipelineExpressions<Car>, CarFilterModel> _currentDataHandler;

        public CarSelectionPipelineBuilder(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public IPipelineBuilder<SelectionPipelineExpressions<Car>, CarFilterModel> SetFirstChainPart<T>() where T : IDataHandler<SelectionPipelineExpressions<Car>, CarFilterModel> {

            var dataHandler = _serviceProvider.GetRequiredService<T>();
            _firstDataHandler = _currentDataHandler = dataHandler;
            return this;
        }

        public IPipelineBuilder<SelectionPipelineExpressions<Car>, CarFilterModel> SetNextChainPart<T>() where T : IDataHandler<SelectionPipelineExpressions<Car>, CarFilterModel> {

            var dataHandler = _serviceProvider.GetRequiredService<T>();
            _currentDataHandler = _currentDataHandler.SetNext(dataHandler);
            return this;
        }

        public IDataHandler<SelectionPipelineExpressions<Car>, CarFilterModel> GetPipeline() {
            return _firstDataHandler;
        }
    }
}