using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;
using CourseProject.Domain;

namespace CourseProject.BLL.DataHandlers.SupplyOrderDataHandlers; 

public class SupplyOrderStateFilterDataHandler : DataHandler<SupplyOrder, SupplyOrderFilterModel> {
    public override void AddExpression(SelectionPipelineExpressions<SupplyOrder> expressions, SupplyOrderFilterModel filterModel) {

        switch (filterModel.State) {
            case SupplyOrderState.New:
                expressions.FilterExpressions.Add(p => p.State == SupplyOrderState.New);
                break;
            case SupplyOrderState.Processing:
                expressions.FilterExpressions.Add(p => p.State == SupplyOrderState.Processing);
                break;
            case SupplyOrderState.Closed:
                expressions.FilterExpressions.Add(p => p.State == SupplyOrderState.Closed);
                break;
            case SupplyOrderState.Canceled:
                expressions.FilterExpressions.Add(p => p.State == SupplyOrderState.Canceled);
                break;
        }

        base.AddExpression(expressions, filterModel);
    }
}