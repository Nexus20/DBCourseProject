using System.Linq.Expressions;

namespace CourseProject.DAL.SelectionPipelineExpressions {
    public class SelectionPipelineExpressions<TEntity> where TEntity : class{

        public List<Expression<Func<TEntity, bool>>> FilterExpressions { get; set; } = new();

        public List<Expression<Func<TEntity, object>>> AscendingOrderExpressions { get; set; } = new();

        public List<Expression<Func<TEntity, object>>> DescendingOrderExpressions { get; set; } = new();

        public int TakeCount { get; set; }

        public int SkipCount { get; set; }
    }
}