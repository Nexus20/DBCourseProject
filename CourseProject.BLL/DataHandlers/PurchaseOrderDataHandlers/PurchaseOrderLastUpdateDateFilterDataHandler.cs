using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers.PurchaseOrderDataHandlers; 

public class PurchaseOrderLastUpdateDateFilterDataHandler : DataHandler<PurchaseOrder, PurchaseOrderFilterModel> {
    public override void AddExpression(SelectionPipelineExpressions<PurchaseOrder> expressions, PurchaseOrderFilterModel filterModel) {

        if (filterModel.LastUpdateDate > DateTime.UnixEpoch) {
            expressions.FilterExpressions.Add(p => p.LastUpdateDate.Day == filterModel.LastUpdateDate.Day && p.LastUpdateDate.Month == filterModel.LastUpdateDate.Month && p.LastUpdateDate.Year == filterModel.LastUpdateDate.Year);
        }

        base.AddExpression(expressions, filterModel);
    }
}