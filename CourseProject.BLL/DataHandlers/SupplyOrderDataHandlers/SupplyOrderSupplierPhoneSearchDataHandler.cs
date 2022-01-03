using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers.SupplyOrderDataHandlers; 

public class SupplyOrderSupplierPhoneSearchDataHandler : DataHandler<SupplyOrder, SupplyOrderFilterModel> {

    public override void AddExpression(SelectionPipelineExpressions<SupplyOrder> expressions, SupplyOrderFilterModel filterModel) {

        if (!string.IsNullOrWhiteSpace(filterModel.SupplierPhone)) {
            expressions.FilterExpressions.Add(p => p.Supplier.Phone.Contains(filterModel.SupplierPhone));
        }

        base.AddExpression(expressions, filterModel);
    }
}