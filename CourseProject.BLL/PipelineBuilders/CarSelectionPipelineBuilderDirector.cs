using CourseProject.BLL.DataHandlers;
using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.PipelineBuilders {
    public class CarSelectionPipelineBuilderDirector : IPipelineBuilderDirector<SelectionPipelineExpressions<Car>, CarFilterModel> {

        private readonly IPipelineBuilder<SelectionPipelineExpressions<Car>, CarFilterModel> _selectionPipelineBuilder;

        public CarSelectionPipelineBuilderDirector(IPipelineBuilder<SelectionPipelineExpressions<Car>, CarFilterModel> selectionPipelineBuilder) {
            _selectionPipelineBuilder = selectionPipelineBuilder;
        }

        public IDataHandler<SelectionPipelineExpressions<Car>, CarFilterModel> Construct() {

            _selectionPipelineBuilder.SetFirstChainPart<BrandModelFilterDataHandler>()
                .SetNextChainPart<AlphabetOrderDataHandler>()
                .SetNextChainPart<SkipGamesDataHandler>()
                .SetNextChainPart<TakeGamesDataHandler>();

            return _selectionPipelineBuilder.GetPipeline();
        }
    }
}