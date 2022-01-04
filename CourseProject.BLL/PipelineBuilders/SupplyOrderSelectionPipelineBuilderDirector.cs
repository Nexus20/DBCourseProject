using CourseProject.BLL.DataHandlers;
using CourseProject.BLL.DataHandlers.SupplyOrderDataHandlers;
using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;

namespace CourseProject.BLL.PipelineBuilders {
    public class SupplyOrderSelectionPipelineBuilderDirector : IPipelineBuilderDirector<SupplyOrder, SupplyOrderFilterModel> {

        private readonly IPipelineBuilder<SupplyOrder, SupplyOrderFilterModel> _selectionPipelineBuilder;

        public SupplyOrderSelectionPipelineBuilderDirector(IPipelineBuilder<SupplyOrder, SupplyOrderFilterModel> selectionPipelineBuilder) {
            _selectionPipelineBuilder = selectionPipelineBuilder;
        }

        public IDataHandler<SupplyOrder, SupplyOrderFilterModel> Construct() {

            _selectionPipelineBuilder.SetFirstChainPart<SupplyOrderOrderIdFilterDataHandler>()
                .SetNextChainPart<SupplyOrderSupplierIdFilterDataHandler>()
                .SetNextChainPart<SupplyOrderSupplierEmailSearchDataHandler>()
                .SetNextChainPart<SupplyOrderSupplierPhoneSearchDataHandler>()
                .SetNextChainPart<SupplyOrderSupplierNameSearchDataHandler>()
                .SetNextChainPart<SupplyOrderCreationDateFilterDataHandler>()
                .SetNextChainPart<SupplyOrderLastUpdateDateFilterDataHandler>()
                .SetNextChainPart<SupplyOrderManagerFilterDataHandler>()
                .SetNextChainPart<SupplyOrderStateFilterDataHandler>()
                .SetNextChainPart<SupplyOrderOrderDataHandler>()
                .SetNextChainPart<SkipObjectsDataHandler<SupplyOrder, SupplyOrderFilterModel>>()
                .SetNextChainPart<TakeObjectsDataHandler<SupplyOrder, SupplyOrderFilterModel>>();

            return _selectionPipelineBuilder.GetPipeline();
        }
    }
}