using CourseProject.BLL.DataHandlers;
using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace CourseProject.BLL.PipelineBuilders {
    public class SupplierSelectionPipelineBuilder : IPipelineBuilder<Supplier, SupplierFilterModel> {

        private readonly IServiceProvider _serviceProvider;

        private IDataHandler<Supplier, SupplierFilterModel> _firstDataHandler;

        private IDataHandler<Supplier, SupplierFilterModel> _currentDataHandler;

        public SupplierSelectionPipelineBuilder(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public IPipelineBuilder<Supplier, SupplierFilterModel> SetFirstChainPart<T>() where T : IDataHandler<Supplier, SupplierFilterModel> {

            var dataHandler = _serviceProvider.GetRequiredService<T>();
            _firstDataHandler = _currentDataHandler = dataHandler;
            return this;
        }

        public IPipelineBuilder<Supplier, SupplierFilterModel> SetNextChainPart<T>() where T : IDataHandler<Supplier, SupplierFilterModel> {

            var dataHandler = _serviceProvider.GetRequiredService<T>();
            _currentDataHandler = _currentDataHandler.SetNext(dataHandler);
            return this;
        }

        public IDataHandler<Supplier, SupplierFilterModel> GetPipeline() {
            return _firstDataHandler;
        }
    }
}