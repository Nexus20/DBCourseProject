namespace CourseProject.BLL.DataHandlers {
    public interface IDataHandler<TExpressions, TFilterModel> {

        IDataHandler<TExpressions, TFilterModel> SetNext(IDataHandler<TExpressions, TFilterModel> nextHandler);

        void AddExpression(TExpressions expressions, TFilterModel filterModel);
    }
}