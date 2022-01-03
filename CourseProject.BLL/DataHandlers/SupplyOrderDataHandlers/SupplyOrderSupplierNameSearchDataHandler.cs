using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers.SupplyOrderDataHandlers; 

public class SupplyOrderSupplierNameSearchDataHandler : DataHandler<SupplyOrder, SupplyOrderFilterModel> {
    public override void AddExpression(SelectionPipelineExpressions<SupplyOrder> expressions, SupplyOrderFilterModel filterModel) {

        if (!string.IsNullOrWhiteSpace(filterModel.SupplierName)) {
            expressions.FilterExpressions.Add(p => p.Supplier.Name.Contains(filterModel.SupplierName));
        }

        base.AddExpression(expressions, filterModel);
    }
}