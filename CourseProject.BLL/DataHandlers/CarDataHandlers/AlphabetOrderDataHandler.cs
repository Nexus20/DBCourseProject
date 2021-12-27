using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers.CarDataHandlers {

    public class AlphabetOrderDataHandler : DataHandler<Car, CarFilterModel> {

        public override void AddExpression(SelectionPipelineExpressions<Car> expressions, CarFilterModel filterModel) {

            switch (filterModel.OrderType) {
                case CarOrderType.AlphabetAsc:
                    expressions.AscendingOrderExpressions.Add(c => c.Model.Brand.Name);
                    expressions.AscendingOrderExpressions.Add(c => c.Model.Name);
                    expressions.AscendingOrderExpressions.Add(c => c.Submodel);
                    break;
                case CarOrderType.AlphabetDesc:
                    expressions.DescendingOrderExpressions.Add(c => c.Model.Brand.Name);
                    expressions.DescendingOrderExpressions.Add(c => c.Model.Name);
                    expressions.DescendingOrderExpressions.Add(c => c.Submodel);
                    break;
            }

            base.AddExpression(expressions, filterModel);
        }
    }
}