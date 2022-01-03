using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers.CarInStockDataHandlers; 

public class CarInStockBrandModelFilterDataHandler : DataHandler<CarInStock, CarInStockFilterModel> {
    public override void AddExpression(SelectionPipelineExpressions<CarInStock> expressions, CarInStockFilterModel filterModel) {

        if (filterModel.BrandId == 0 && filterModel.ModelId > 0) {
            expressions.FilterExpressions.Add(c => c.Car.ModelId == filterModel.ModelId);

        } else if (filterModel.ModelId == 0 && filterModel.BrandId > 0) {
            expressions.FilterExpressions.Add(c => c.Car.Model.BrandId == filterModel.BrandId);
        }
        else if(filterModel.ModelId > 0 && filterModel.BrandId > 0) {
            expressions.FilterExpressions.Add(c => c.Car.ModelId == filterModel.ModelId && c.Car.Model.BrandId == filterModel.BrandId);
        }

        base.AddExpression(expressions, filterModel);
    }
}