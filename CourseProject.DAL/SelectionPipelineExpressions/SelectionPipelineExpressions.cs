using System.Linq.Expressions;

namespace CourseProject.DAL.SelectionPipelineExpressions {
    public class SelectionPipelineExpressions<TEntity> where TEntity : class{

        public List<Expression<Func<TEntity, bool>>> FilterExpressions { get; set; } = new();

        public Expression<Func<TEntity, object>> AscendingOrderExpression { get; set; }

        public Expression<Func<TEntity, object>> DescendingOrderExpression { get; set; }

        public int TakeCount { get; set; }

        public int SkipCount { get; set; }
    }
}