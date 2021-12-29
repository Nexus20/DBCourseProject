using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers.SupplierDataHandlers; 

public class SupplierPhoneSearchDataHandler : DataHandler<Supplier, SupplierFilterModel> {
    public override void AddExpression(SelectionPipelineExpressions<Supplier> expressions, SupplierFilterModel filterModel) {

        if (!string.IsNullOrWhiteSpace(filterModel.Phone)) {
            expressions.FilterExpressions.Add(s => s.Phone.Contains(filterModel.Phone));
        }

        base.AddExpression(expressions, filterModel);
    }
}