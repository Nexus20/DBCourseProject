using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers.PurchaseOrderDataHandlers; 

public class PurchaseOrderCreationDateFilterDataHandler : DataHandler<PurchaseOrder, PurchaseOrderFilterModel> {
    public override void AddExpression(SelectionPipelineExpressions<PurchaseOrder> expressions, PurchaseOrderFilterModel filterModel) {

        if (filterModel.CreationDate > DateTime.UnixEpoch) {
            expressions.FilterExpressions.Add(p => p.CreationDate.Day == filterModel.CreationDate.Day && p.CreationDate.Month == filterModel.CreationDate.Month && p.CreationDate.Year == filterModel.CreationDate.Year);
        }

        base.AddExpression(expressions, filterModel);
    }
}