using CourseProject.BLL.DataHandlers;
using CourseProject.BLL.DataHandlers.SupplierDataHandlers;
using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;

namespace CourseProject.BLL.PipelineBuilders {
    public class SupplierSelectionPipelineBuilderDirector : IPipelineBuilderDirector<Supplier, SupplierFilterModel> {

        private readonly IPipelineBuilder<Supplier, SupplierFilterModel> _selectionPipelineBuilder;

        public SupplierSelectionPipelineBuilderDirector(IPipelineBuilder<Supplier, SupplierFilterModel> selectionPipelineBuilder) {
            _selectionPipelineBuilder = selectionPipelineBuilder;
        }

        public IDataHandler<Supplier, SupplierFilterModel> Construct() {

            _selectionPipelineBuilder.SetFirstChainPart<SupplierBrandFilterDataHandler>()
                .SetNextChainPart<SupplierNameSearchDataHandler>()
                .SetNextChainPart<SupplierPhoneSearchDataHandler>()
                .SetNextChainPart<SupplierEmailSearchDataHandler>()
                .SetNextChainPart<SupplierOrderDataHandler>()
                .SetNextChainPart<SkipObjectsDataHandler<Supplier, SupplierFilterModel>>()
                .SetNextChainPart<TakeObjectsDataHandler<Supplier, SupplierFilterModel>>();

            return _selectionPipelineBuilder.GetPipeline();
        }
    }
}