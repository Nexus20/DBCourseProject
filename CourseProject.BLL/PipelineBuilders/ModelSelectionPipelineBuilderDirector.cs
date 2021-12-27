using CourseProject.BLL.DataHandlers;
using CourseProject.BLL.DataHandlers.ModelDataHandlers;
using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;

namespace CourseProject.BLL.PipelineBuilders {
    public class ModelSelectionPipelineBuilderDirector : IPipelineBuilderDirector<Model, ModelFilterModel> {

        private readonly IPipelineBuilder<Model, ModelFilterModel> _selectionPipelineBuilder;

        public ModelSelectionPipelineBuilderDirector(IPipelineBuilder<Model, ModelFilterModel> selectionPipelineBuilder) {
            _selectionPipelineBuilder = selectionPipelineBuilder;
        }

        public IDataHandler<Model, ModelFilterModel> Construct() {

            _selectionPipelineBuilder.SetFirstChainPart<BrandFilterDataHandler>()
                .SetNextChainPart<ModelSearchDataHandler>()
                .SetNextChainPart<AlphabetOrderDataHandler>()
                .SetNextChainPart<SkipGamesDataHandler<Model, ModelFilterModel>>()
                .SetNextChainPart<TakeGamesDataHandler<Model, ModelFilterModel>>();

            return _selectionPipelineBuilder.GetPipeline();
        }
    }
}