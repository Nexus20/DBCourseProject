using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers.PurchaseOrderDataHandlers; 

public class PurchaseOrderManagerFilterDataHandler : DataHandler<PurchaseOrder, PurchaseOrderFilterModel> {
    public override void AddExpression(SelectionPipelineExpressions<PurchaseOrder> expressions, PurchaseOrderFilterModel filterModel) {

        if (!string.IsNullOrWhiteSpace(filterModel.ManagerId)) {
            expressions.FilterExpressions.Add(p => p.ManagerId.ToString() == filterModel.ManagerId);
        }

        base.AddExpression(expressions, filterModel);
    }
}