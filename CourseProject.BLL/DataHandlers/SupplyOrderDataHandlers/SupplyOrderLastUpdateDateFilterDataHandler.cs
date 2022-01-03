using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers.SupplyOrderDataHandlers; 

public class SupplyOrderLastUpdateDateFilterDataHandler : DataHandler<SupplyOrder, SupplyOrderFilterModel> {
    public override void AddExpression(SelectionPipelineExpressions<SupplyOrder> expressions, SupplyOrderFilterModel filterModel) {

        if (filterModel.LastUpdateDate > DateTime.UnixEpoch) {
            expressions.FilterExpressions.Add(p => p.LastUpdateDate.Day == filterModel.LastUpdateDate.Day && p.LastUpdateDate.Month == filterModel.LastUpdateDate.Month && p.LastUpdateDate.Year == filterModel.LastUpdateDate.Year);
        }

        base.AddExpression(expressions, filterModel);
    }
}