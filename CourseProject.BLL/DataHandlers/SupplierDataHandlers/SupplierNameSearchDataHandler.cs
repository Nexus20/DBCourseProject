using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers.SupplierDataHandlers; 

public class SupplierNameSearchDataHandler : DataHandler<Supplier, SupplierFilterModel> {
    public override void AddExpression(SelectionPipelineExpressions<Supplier> expressions, SupplierFilterModel filterModel) {

        if (!string.IsNullOrWhiteSpace(filterModel.Name)) {
            expressions.FilterExpressions.Add(s => s.Name.Contains(filterModel.Name));
        }

        base.AddExpression(expressions, filterModel);
    }
}