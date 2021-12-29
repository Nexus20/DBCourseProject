using CourseProject.BLL.FilterModels;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers {
    public class TakeObjectsDataHandler<TEntity, TFilterModel> : DataHandler<TEntity, TFilterModel> where TEntity : class where TFilterModel : FilterModel{

        public override void AddExpression(SelectionPipelineExpressions<TEntity> expressions, TFilterModel filterModel) {

            if (filterModel.TakeCount > 0) {
                expressions.TakeCount = filterModel.TakeCount;
            }

            base.AddExpression(expressions, filterModel);
        }
    }
}