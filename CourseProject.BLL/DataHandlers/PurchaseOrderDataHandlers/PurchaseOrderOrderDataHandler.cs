using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers.PurchaseOrderDataHandlers {

    public class PurchaseOrderOrderDataHandler : DataHandler<PurchaseOrder, PurchaseOrderFilterModel> {

        public override void AddExpression(SelectionPipelineExpressions<PurchaseOrder> expressions, PurchaseOrderFilterModel filterModel) {

            switch (filterModel.OrderType) {
                case PurchaseOrderOrderType.OrderIdAsc:
                    expressions.AscendingOrderExpressions.Add(p => p.Id);
                    break;
                case PurchaseOrderOrderType.OrderIdDesc:
                    expressions.DescendingOrderExpressions.Add(p => p.Id);
                    break;
                case PurchaseOrderOrderType.CreationDateAsc:
                    expressions.AscendingOrderExpressions.Add(p => p.CreationDate);
                    break;
                case PurchaseOrderOrderType.CreationDateDesc:
                    expressions.DescendingOrderExpressions.Add(p => p.CreationDate);
                    break;
                case PurchaseOrderOrderType.LastUpdateDateAsc:
                    expressions.AscendingOrderExpressions.Add(p => p.LastUpdateDate);
                    break;
                case PurchaseOrderOrderType.LastUpdateDateDesc:
                    expressions.DescendingOrderExpressions.Add(p => p.LastUpdateDate);
                    break;
            }

            base.AddExpression(expressions, filterModel);
        }
    }
}