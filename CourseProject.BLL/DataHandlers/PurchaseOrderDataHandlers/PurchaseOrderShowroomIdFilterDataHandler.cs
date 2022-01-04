using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers.PurchaseOrderDataHandlers; 

public class PurchaseOrderShowroomIdFilterDataHandler : DataHandler<PurchaseOrder, PurchaseOrderFilterModel> {
    public override void AddExpression(SelectionPipelineExpressions<PurchaseOrder> expressions, PurchaseOrderFilterModel filterModel) {

        if (filterModel.ShowroomId > 0) {
            expressions.FilterExpressions.Add(p => p.ShowroomId == filterModel.ShowroomId);
        }

        base.AddExpression(expressions, filterModel);
    }
}