using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers.CarDataHandlers; 

public class CarBrandModelFilterDataHandler : DataHandler<Car, CarFilterModel> {
    public override void AddExpression(SelectionPipelineExpressions<Car> expressions, CarFilterModel filterModel) {

        if (filterModel.BrandId == 0 && filterModel.ModelId > 0) {
            expressions.FilterExpressions.Add(c => c.ModelId == filterModel.ModelId);

        } else if (filterModel.ModelId == 0 && filterModel.BrandId > 0) {
            expressions.FilterExpressions.Add(c => c.Model.BrandId == filterModel.BrandId);
        }
        else if(filterModel.ModelId > 0 && filterModel.BrandId > 0) {
            expressions.FilterExpressions.Add(c => c.ModelId == filterModel.ModelId && c.Model.BrandId == filterModel.BrandId);
        }

        base.AddExpression(expressions, filterModel);
    }
}