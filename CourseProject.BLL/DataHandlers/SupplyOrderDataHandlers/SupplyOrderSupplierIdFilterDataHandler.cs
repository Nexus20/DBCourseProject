using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers.SupplyOrderDataHandlers; 

public class SupplyOrderSupplierIdFilterDataHandler : DataHandler<SupplyOrder, SupplyOrderFilterModel> {
    public override void AddExpression(SelectionPipelineExpressions<SupplyOrder> expressions, SupplyOrderFilterModel filterModel) {

        if (filterModel.SupplierId > 0) {
            expressions.FilterExpressions.Add(p => p.SupplierId == filterModel.SupplierId);
        }

        base.AddExpression(expressions, filterModel);
    }
}