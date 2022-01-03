using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers.CarInStockDataHandlers; 

public class CarInStockShowroomFilterDataHandler : DataHandler<CarInStock, CarInStockFilterModel> {
    public override void AddExpression(SelectionPipelineExpressions<CarInStock> expressions, CarInStockFilterModel filterModel) {

        if (filterModel.ShowroomId > 0) {
            expressions.FilterExpressions.Add(c => c.ShowroomId == filterModel.ShowroomId);
        }

        base.AddExpression(expressions, filterModel);
    }
}