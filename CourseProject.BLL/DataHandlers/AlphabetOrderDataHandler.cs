using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers {

    public class AlphabetOrderDataHandler : DataHandler<SelectionPipelineExpressions<Car>, CarFilterModel> {

        public override void AddExpression(SelectionPipelineExpressions<Car> expressions, CarFilterModel filterModel) {

            if (filterModel.OrderType != CarOrderType.None) {

                if (filterModel.OrderType != CarOrderType.AlphabetAsc) {

                    expressions.AscendingOrderExpression = c => $"{c.Model.Brand}{c.Model.Name}{c.Submodel}";

                } else if (filterModel.OrderType != CarOrderType.AlphabetDesc) {

                    expressions.DescendingOrderExpression = c => $"{c.Model.Brand}{c.Model.Name}{c.Submodel}";
                }
            }

            base.AddExpression(expressions, filterModel);
        }
    }
}