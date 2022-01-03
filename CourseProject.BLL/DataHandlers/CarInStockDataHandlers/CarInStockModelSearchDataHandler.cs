using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers.CarInStockDataHandlers; 

public class CarInStockModelSearchDataHandler : DataHandler<CarInStock, CarInStockFilterModel> {
    public override void AddExpression(SelectionPipelineExpressions<CarInStock> expressions, CarInStockFilterModel filterModel) {

        if (!string.IsNullOrWhiteSpace(filterModel.Model)) {
            expressions.FilterExpressions.Add(c => c.Car.Model.Brand.Name.Contains(filterModel.Model) || c.Car.Model.Name.Contains(filterModel.Model) || c.Car.Submodel.Contains(filterModel.Model));
        }

        base.AddExpression(expressions, filterModel);
    }
}