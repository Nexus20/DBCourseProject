using CourseProject.BLL.DataHandlers;
using CourseProject.BLL.DataHandlers.BrandDataHandlers;
using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;

namespace CourseProject.BLL.PipelineBuilders {
    public class BrandSelectionPipelineBuilderDirector : IPipelineBuilderDirector<Brand, BrandFilterModel> {

        private readonly IPipelineBuilder<Brand, BrandFilterModel> _selectionPipelineBuilder;

        public BrandSelectionPipelineBuilderDirector(IPipelineBuilder<Brand, BrandFilterModel> selectionPipelineBuilder) {
            _selectionPipelineBuilder = selectionPipelineBuilder;
        }

        public IDataHandler<Brand, BrandFilterModel> Construct() {

            _selectionPipelineBuilder.SetFirstChainPart<BrandSearchDataHandler>()
                .SetNextChainPart<BrandOrderDataHandler>()
                .SetNextChainPart<SkipObjectsDataHandler<Brand, BrandFilterModel>>()
                .SetNextChainPart<TakeObjectsDataHandler<Brand, BrandFilterModel>>();

            return _selectionPipelineBuilder.GetPipeline();
        }
    }
}