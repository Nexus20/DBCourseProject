using CourseProject.BLL.DataHandlers;
using CourseProject.BLL.DataHandlers.CarInStockDataHandlers;
using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;

namespace CourseProject.BLL.PipelineBuilders {
    public class CarInStockSelectionPipelineBuilderDirector : IPipelineBuilderDirector<CarInStock, CarInStockFilterModel> {

        private readonly IPipelineBuilder<CarInStock, CarInStockFilterModel> _selectionPipelineBuilder;

        public CarInStockSelectionPipelineBuilderDirector(IPipelineBuilder<CarInStock, CarInStockFilterModel> selectionPipelineBuilder) {
            _selectionPipelineBuilder = selectionPipelineBuilder;
        }

        public IDataHandler<CarInStock, CarInStockFilterModel> Construct() {

            _selectionPipelineBuilder.SetFirstChainPart<CarInStockShowroomFilterDataHandler>()
                .SetNextChainPart<CarInStockBrandModelFilterDataHandler>()
                .SetNextChainPart<CarInStockModelSearchDataHandler>()
                .SetNextChainPart<CarInStockOrderDataHandler>()
                .SetNextChainPart<SkipObjectsDataHandler<CarInStock, CarInStockFilterModel>>()
                .SetNextChainPart<TakeObjectsDataHandler<CarInStock, CarInStockFilterModel>>();

            return _selectionPipelineBuilder.GetPipeline();
        }
    }
}