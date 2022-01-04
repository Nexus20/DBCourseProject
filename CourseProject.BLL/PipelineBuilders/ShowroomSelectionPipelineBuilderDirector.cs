using CourseProject.BLL.DataHandlers;
using CourseProject.BLL.DataHandlers.EquipmentItemCategoryDataHandlers;
using CourseProject.BLL.DataHandlers.ShowroomDataHandlers;
using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;

namespace CourseProject.BLL.PipelineBuilders {
    public class ShowroomSelectionPipelineBuilderDirector : IPipelineBuilderDirector<Showroom, ShowroomFilterModel> {

        private readonly IPipelineBuilder<Showroom, ShowroomFilterModel> _selectionPipelineBuilder;

        public ShowroomSelectionPipelineBuilderDirector(IPipelineBuilder<Showroom, ShowroomFilterModel> selectionPipelineBuilder) {
            _selectionPipelineBuilder = selectionPipelineBuilder;
        }

        public IDataHandler<Showroom, ShowroomFilterModel> Construct() {

            _selectionPipelineBuilder.SetFirstChainPart<ShowroomAddressSearchDataHandler>()
                .SetNextChainPart<ShowroomPhoneSearchDataHandler>()
                .SetNextChainPart<ShowroomOrderDataHandler>()
                .SetNextChainPart<SkipObjectsDataHandler<Showroom, ShowroomFilterModel>>()
                .SetNextChainPart<TakeObjectsDataHandler<Showroom, ShowroomFilterModel>>();

            return _selectionPipelineBuilder.GetPipeline();
        }
    }
}