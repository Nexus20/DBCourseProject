using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers.ModelDataHandlers; 

public class ModelBrandFilterDataHandler : DataHandler<Model, ModelFilterModel> {
    public override void AddExpression(SelectionPipelineExpressions<Model> expressions, ModelFilterModel filterModel) {

        if (filterModel.BrandId > 0) {
            expressions.FilterExpressions.Add(m => m.BrandId == filterModel.BrandId);
        }

        base.AddExpression(expressions, filterModel);
    }
}