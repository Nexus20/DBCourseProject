using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers.SupplierDataHandlers; 

public class SupplierBrandFilterDataHandler : DataHandler<Supplier, SupplierFilterModel> {
    public override void AddExpression(SelectionPipelineExpressions<Supplier> expressions, SupplierFilterModel filterModel) {

        if (filterModel.BrandId > 0) {
            expressions.FilterExpressions.Add(m => m.BrandId == filterModel.BrandId);
        }

        base.AddExpression(expressions, filterModel);
    }
}