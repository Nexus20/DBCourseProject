using CourseProject.BLL.DataHandlers;
using CourseProject.BLL.DataHandlers.EquipmentItemCategoryDataHandlers;
using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;

namespace CourseProject.BLL.PipelineBuilders {
    public class EquipmentItemCategorySelectionPipelineBuilderDirector : IPipelineBuilderDirector<EquipmentItemCategory, EquipmentItemCategoryFilterModel> {

        private readonly IPipelineBuilder<EquipmentItemCategory, EquipmentItemCategoryFilterModel> _selectionPipelineBuilder;

        public EquipmentItemCategorySelectionPipelineBuilderDirector(IPipelineBuilder<EquipmentItemCategory, EquipmentItemCategoryFilterModel> selectionPipelineBuilder) {
            _selectionPipelineBuilder = selectionPipelineBuilder;
        }

        public IDataHandler<EquipmentItemCategory, EquipmentItemCategoryFilterModel> Construct() {

            _selectionPipelineBuilder.SetFirstChainPart<EquipmentItemCategoryNameDataHandler>()
                .SetNextChainPart<EquipmentItemCategoryUnitsOfMeasureSearchDataHandler>()
                .SetNextChainPart<EquipmentItemCategoryOrderDataHandler>()
                .SetNextChainPart<SkipObjectsDataHandler<EquipmentItemCategory, EquipmentItemCategoryFilterModel>>()
                .SetNextChainPart<TakeObjectsDataHandler<EquipmentItemCategory, EquipmentItemCategoryFilterModel>>();

            return _selectionPipelineBuilder.GetPipeline();
        }
    }
}