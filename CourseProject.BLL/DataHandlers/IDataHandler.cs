using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers {
    public interface IDataHandler<TEntity, TFilterModel> where TEntity : class {

        IDataHandler<TEntity, TFilterModel> SetNext(IDataHandler<TEntity, TFilterModel> nextHandler);

        void AddExpression(SelectionPipelineExpressions<TEntity> expressions, TFilterModel filterModel);
    }
}