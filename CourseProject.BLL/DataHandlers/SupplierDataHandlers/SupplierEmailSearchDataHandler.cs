using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers.SupplierDataHandlers; 

public class SupplierEmailSearchDataHandler : DataHandler<Supplier, SupplierFilterModel> {
    public override void AddExpression(SelectionPipelineExpressions<Supplier> expressions, SupplierFilterModel filterModel) {

        if (!string.IsNullOrWhiteSpace(filterModel.Email)) {
            expressions.FilterExpressions.Add(s => s.Email.Contains(filterModel.Email));
        }

        base.AddExpression(expressions, filterModel);
    }
}