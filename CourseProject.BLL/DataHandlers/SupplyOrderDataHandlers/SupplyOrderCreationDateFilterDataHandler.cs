using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers.SupplyOrderDataHandlers; 

public class SupplyOrderCreationDateFilterDataHandler : DataHandler<SupplyOrder, SupplyOrderFilterModel> {
    public override void AddExpression(SelectionPipelineExpressions<SupplyOrder> expressions, SupplyOrderFilterModel filterModel) {

        if (filterModel.CreationDate > DateTime.UnixEpoch) {
            expressions.FilterExpressions.Add(p => p.CreationDate.Day == filterModel.CreationDate.Day && p.CreationDate.Month == filterModel.CreationDate.Month && p.CreationDate.Year == filterModel.CreationDate.Year);
        }

        base.AddExpression(expressions, filterModel);
    }
}