using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers {
    public class TakeGamesDataHandler : DataHandler<SelectionPipelineExpressions<Car>, CarFilterModel> {

        public override void AddExpression(SelectionPipelineExpressions<Car> expressions, CarFilterModel filterModel) {

            if (filterModel.TakeCount is > 0) {
                expressions.TakeCount = filterModel.TakeCount.Value;
            }

            base.AddExpression(expressions, filterModel);
        }
    }
}