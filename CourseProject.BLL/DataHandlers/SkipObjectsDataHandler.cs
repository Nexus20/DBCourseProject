using CourseProject.BLL.FilterModels;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers {
    public class SkipObjectsDataHandler<TEntity, TFilterModel> : DataHandler<TEntity, TFilterModel> where TEntity : class where TFilterModel : FilterModel {

        public override void AddExpression(SelectionPipelineExpressions<TEntity> expressions, TFilterModel filterModel) {

            if (filterModel.PageNumber > 1) {
                expressions.SkipCount = filterModel.TakeCount * (filterModel.PageNumber - 1);
            }

            base.AddExpression(expressions, filterModel);
        }
    }
}