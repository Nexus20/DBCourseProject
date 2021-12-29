using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers.BrandDataHandlers; 

public class BrandSearchDataHandler : DataHandler<Brand, BrandFilterModel> {
    public override void AddExpression(SelectionPipelineExpressions<Brand> expressions, BrandFilterModel filterModel) {

        if (!string.IsNullOrWhiteSpace(filterModel.Brand)) {
            expressions.FilterExpressions.Add(b => b.Name.Contains(filterModel.Brand));
        }

        base.AddExpression(expressions, filterModel);
    }
}