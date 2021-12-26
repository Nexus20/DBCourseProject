using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers {
    public class SkipGamesDataHandler : DataHandler<SelectionPipelineExpressions<Car>, CarFilterModel> {

        public override void AddExpression(SelectionPipelineExpressions<Car> expressions, CarFilterModel filterModel) {

            if (filterModel.SkipCount is > 0) {
                expressions.SkipCount = filterModel.SkipCount.Value;
            }

            base.AddExpression(expressions, filterModel);
        }
    }
}