using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers.SupplierDataHandlers {

    public class SupplierOrderDataHandler : DataHandler<Supplier, SupplierFilterModel> {

        public override void AddExpression(SelectionPipelineExpressions<Supplier> expressions, SupplierFilterModel filterModel) {

            switch (filterModel.OrderType) {
                case SupplierOrderType.AlphabetAsc:
                    expressions.AscendingOrderExpressions.Add(s => s.Name);
                    break;
                case SupplierOrderType.AlphabetDesc:
                    expressions.DescendingOrderExpressions.Add(s => s.Name);
                    break;
            }

            base.AddExpression(expressions, filterModel);
        }
    }
}