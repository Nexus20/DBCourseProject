using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers.PurchaseOrderDataHandlers; 

public class PurchaseOrderClientEmailSearchDataHandler : DataHandler<PurchaseOrder, PurchaseOrderFilterModel> {
    public override void AddExpression(SelectionPipelineExpressions<PurchaseOrder> expressions, PurchaseOrderFilterModel filterModel) {

        if (!string.IsNullOrWhiteSpace(filterModel.ClientEmail)) {
            expressions.FilterExpressions.Add(p => p.Client.Email.Contains(filterModel.ClientEmail));
        }

        base.AddExpression(expressions, filterModel);
    }
}