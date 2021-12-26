using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers {
    public class SkipGamesDataHandler : DataHandler<SelectionPipelineExpressions<Car>, CarFilterModel> {

        public override void AddExpression(SelectionPipelineExpressions<Car> expressions, CarFilterModel filterModel) {

            if (filterModel.PageNumber > 1) {
                expressions.SkipCount = filterModel.TakeCount * (filterModel.PageNumber - 1);
            }

            base.AddExpression(expressions, filterModel);
        }
    }
}