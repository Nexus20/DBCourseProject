using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers.SupplyOrderDataHandlers; 

public class SupplyOrderManagerFilterDataHandler : DataHandler<SupplyOrder, SupplyOrderFilterModel> {
    public override void AddExpression(SelectionPipelineExpressions<SupplyOrder> expressions, SupplyOrderFilterModel filterModel) {

        if (!string.IsNullOrWhiteSpace(filterModel.ManagerId)) {
            expressions.FilterExpressions.Add(p => p.ManagerId.ToString() == filterModel.ManagerId);
        }

        base.AddExpression(expressions, filterModel);
    }
}