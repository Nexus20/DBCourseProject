using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers.PurchaseOrderDataHandlers; 

public class PurchaseOrderOrderIdFilterDataHandler : DataHandler<PurchaseOrder, PurchaseOrderFilterModel> {
    public override void AddExpression(SelectionPipelineExpressions<PurchaseOrder> expressions, PurchaseOrderFilterModel filterModel) {

        if (filterModel.OrderId > 0) {
            expressions.FilterExpressions.Add(p => p.Id == filterModel.OrderId);
        }

        base.AddExpression(expressions, filterModel);
    }
}