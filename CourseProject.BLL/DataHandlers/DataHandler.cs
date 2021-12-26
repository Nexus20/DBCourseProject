namespace CourseProject.BLL.DataHandlers {
    public abstract class DataHandler<TExpressions, TFilterModel> : IDataHandler<TExpressions, TFilterModel> {

        private IDataHandler<TExpressions, TFilterModel> _nextHandler;

        public IDataHandler<TExpressions, TFilterModel> SetNext(IDataHandler<TExpressions, TFilterModel> nextHandler) {
            _nextHandler = nextHandler;
            return _nextHandler;
        }

        public virtual void AddExpression(TExpressions expressions, TFilterModel filterModel) {
            _nextHandler?.AddExpression(expressions, filterModel);
        }
    }
}