using CourseProject.BLL.DataHandlers;
using CourseProject.BLL.DataHandlers.CarDataHandlers;
using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;

namespace CourseProject.BLL.PipelineBuilders {
    public class CarSelectionPipelineBuilderDirector : IPipelineBuilderDirector<Car, CarFilterModel> {

        private readonly IPipelineBuilder<Car, CarFilterModel> _selectionPipelineBuilder;

        public CarSelectionPipelineBuilderDirector(IPipelineBuilder<Car, CarFilterModel> selectionPipelineBuilder) {
            _selectionPipelineBuilder = selectionPipelineBuilder;
        }

        public IDataHandler<Car, CarFilterModel> Construct() {

            _selectionPipelineBuilder.SetFirstChainPart<CarBrandModelFilterDataHandler>()
                .SetNextChainPart<CarModelSearchDataHandler>()
                .SetNextChainPart<CarOrderDataHandler>()
                .SetNextChainPart<SkipObjectsDataHandler<Car, CarFilterModel>>()
                .SetNextChainPart<TakeObjectsDataHandler<Car, CarFilterModel>>();

            return _selectionPipelineBuilder.GetPipeline();
        }
    }
}