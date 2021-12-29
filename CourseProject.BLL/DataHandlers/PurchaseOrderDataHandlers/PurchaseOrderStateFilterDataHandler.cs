using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;
using CourseProject.Domain;

namespace CourseProject.BLL.DataHandlers.PurchaseOrderDataHandlers; 

public class PurchaseOrderStateFilterDataHandler : DataHandler<PurchaseOrder, PurchaseOrderFilterModel> {
    public override void AddExpression(SelectionPipelineExpressions<PurchaseOrder> expressions, PurchaseOrderFilterModel filterModel) {

        switch (filterModel.State) {
            case PurchaseOrderState.New:
                expressions.FilterExpressions.Add(p => p.State == PurchaseOrderState.New);
                break;
            case PurchaseOrderState.Processing:
                expressions.FilterExpressions.Add(p => p.State == PurchaseOrderState.Processing);
                break;
            case PurchaseOrderState.Closed:
                expressions.FilterExpressions.Add(p => p.State == PurchaseOrderState.Closed);
                break;
            case PurchaseOrderState.Canceled:
                expressions.FilterExpressions.Add(p => p.State == PurchaseOrderState.Canceled);
                break;
        }

        base.AddExpression(expressions, filterModel);
    }
}