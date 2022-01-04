using CourseProject.BLL.DataHandlers;
using CourseProject.BLL.DataHandlers.PurchaseOrderDataHandlers;
using CourseProject.BLL.DataHandlers.SupplierDataHandlers;
using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;

namespace CourseProject.BLL.PipelineBuilders {
    public class PurchaseOrderSelectionPipelineBuilderDirector : IPipelineBuilderDirector<PurchaseOrder, PurchaseOrderFilterModel> {

        private readonly IPipelineBuilder<PurchaseOrder, PurchaseOrderFilterModel> _selectionPipelineBuilder;

        public PurchaseOrderSelectionPipelineBuilderDirector(IPipelineBuilder<PurchaseOrder, PurchaseOrderFilterModel> selectionPipelineBuilder) {
            _selectionPipelineBuilder = selectionPipelineBuilder;
        }

        public IDataHandler<PurchaseOrder, PurchaseOrderFilterModel> Construct() {

            _selectionPipelineBuilder.SetFirstChainPart<PurchaseOrderOrderIdFilterDataHandler>()
                .SetNextChainPart<PurchaseOrderShowroomIdFilterDataHandler>()
                .SetNextChainPart<PurchaseOrderClientEmailSearchDataHandler>()
                .SetNextChainPart<PurchaseOrderClientPhoneSearchDataHandler>()
                .SetNextChainPart<PurchaseOrderCreationDateFilterDataHandler>()
                .SetNextChainPart<PurchaseOrderLastUpdateDateFilterDataHandler>()
                .SetNextChainPart<PurchaseOrderManagerFilterDataHandler>()
                .SetNextChainPart<PurchaseOrderStateFilterDataHandler>()
                .SetNextChainPart<PurchaseOrderOrderDataHandler>()
                .SetNextChainPart<SkipObjectsDataHandler<PurchaseOrder, PurchaseOrderFilterModel>>()
                .SetNextChainPart<TakeObjectsDataHandler<PurchaseOrder, PurchaseOrderFilterModel>>();

            return _selectionPipelineBuilder.GetPipeline();
        }
    }
}