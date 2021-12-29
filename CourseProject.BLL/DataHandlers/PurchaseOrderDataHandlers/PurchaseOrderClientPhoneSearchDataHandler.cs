using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers.PurchaseOrderDataHandlers; 

public class PurchaseOrderClientPhoneSearchDataHandler : DataHandler<PurchaseOrder, PurchaseOrderFilterModel> {
    public override void AddExpression(SelectionPipelineExpressions<PurchaseOrder> expressions, PurchaseOrderFilterModel filterModel) {

        if (!string.IsNullOrWhiteSpace(filterModel.ClientPhone)) {
            expressions.FilterExpressions.Add(p => p.Client.PhoneNumber.Contains(filterModel.ClientPhone));
        }

        base.AddExpression(expressions, filterModel);
    }
}