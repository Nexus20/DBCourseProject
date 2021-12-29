using CourseProject.BLL.FilterModels;
using CourseProject.BLL.PipelineBuilders;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.Pipeline {
    public class SelectionPipeline<TEntity, TFilterModel> where TEntity : class where TFilterModel : FilterModel {

        private readonly TFilterModel _filterModel;

        private readonly IPipelineBuilderDirector<TEntity, TFilterModel> _builderDirector;

        public SelectionPipeline(TFilterModel filterModel, IPipelineBuilderDirector<TEntity, TFilterModel> builderDirector) {
            _filterModel = filterModel;
            _builderDirector = builderDirector;
        }

        public SelectionPipelineExpressions<TEntity> Process() {

            var expressions = new SelectionPipelineExpressions<TEntity>();

            _builderDirector.Construct().AddExpression(expressions, _filterModel);

            return expressions;
        }
    }
}