using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers.CarInStockDataHandlers {

    public class CarInStockOrderDataHandler : DataHandler<CarInStock, CarInStockFilterModel> {

        public override void AddExpression(SelectionPipelineExpressions<CarInStock> expressions, CarInStockFilterModel filterModel) {

            switch (filterModel.OrderType) {
                case CarOrderType.AlphabetAsc:
                    expressions.AscendingOrderExpressions.Add(c => c.Car.Model.Brand.Name);
                    expressions.AscendingOrderExpressions.Add(c => c.Car.Model.Name);
                    expressions.AscendingOrderExpressions.Add(c => c.Car.Submodel);
                    break;
                case CarOrderType.AlphabetDesc:
                    expressions.DescendingOrderExpressions.Add(c => c.Car.Model.Brand.Name);
                    expressions.DescendingOrderExpressions.Add(c => c.Car.Model.Name);
                    expressions.DescendingOrderExpressions.Add(c => c.Car.Submodel);
                    break;
            }

            base.AddExpression(expressions, filterModel);
        }
    }
}