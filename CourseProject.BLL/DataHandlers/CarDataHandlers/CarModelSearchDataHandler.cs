using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers.CarDataHandlers; 

public class CarModelSearchDataHandler : DataHandler<Car, CarFilterModel> {
    public override void AddExpression(SelectionPipelineExpressions<Car> expressions, CarFilterModel filterModel) {

        if (!string.IsNullOrWhiteSpace(filterModel.Model)) {
            expressions.FilterExpressions.Add(c => c.Model.Brand.Name.Contains(filterModel.Model) || c.Model.Name.Contains(filterModel.Model) || c.Submodel.Contains(filterModel.Model));
        }

        base.AddExpression(expressions, filterModel);
    }
}