using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers.ModelDataHandlers; 

public class ModelSearchDataHandler : DataHandler<Model, ModelFilterModel> {
    public override void AddExpression(SelectionPipelineExpressions<Model> expressions, ModelFilterModel filterModel) {

        if (!string.IsNullOrWhiteSpace(filterModel.Model)) {
            expressions.FilterExpressions.Add(m => m.Brand.Name.Contains(filterModel.Model) || m.Name.Contains(filterModel.Model));
        }

        base.AddExpression(expressions, filterModel);
    }
}