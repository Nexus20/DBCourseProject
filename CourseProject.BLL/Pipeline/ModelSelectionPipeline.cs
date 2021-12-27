using CourseProject.BLL.FilterModels;
using CourseProject.BLL.PipelineBuilders;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.Pipeline {
    public class ModelSelectionPipeline : Pipeline<SelectionPipelineExpressions<Model>, ModelFilterModel> {

        private readonly ModelFilterModel _filterModel;

        private readonly IPipelineBuilderDirector<Model, ModelFilterModel> _builderDirector;

        public ModelSelectionPipeline(ModelFilterModel filterModel, IPipelineBuilderDirector<Model, ModelFilterModel> builderDirector) {
            _filterModel = filterModel;
            _builderDirector = builderDirector;
        }

        public override SelectionPipelineExpressions<Model> Process() {

            var expressions = new SelectionPipelineExpressions<Model>();

            _builderDirector.Construct().AddExpression(expressions, _filterModel);

            return expressions;
        }
    }
}