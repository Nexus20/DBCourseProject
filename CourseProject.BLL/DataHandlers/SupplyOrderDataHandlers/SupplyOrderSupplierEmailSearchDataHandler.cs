using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers.SupplyOrderDataHandlers; 

public class SupplyOrderSupplierEmailSearchDataHandler : DataHandler<SupplyOrder, SupplyOrderFilterModel> {
    public override void AddExpression(SelectionPipelineExpressions<SupplyOrder> expressions, SupplyOrderFilterModel filterModel) {

        if (!string.IsNullOrWhiteSpace(filterModel.SupplierEmail)) {
            expressions.FilterExpressions.Add(p => p.Supplier.Email.Contains(filterModel.SupplierEmail));
        }

        base.AddExpression(expressions, filterModel);
    }
}