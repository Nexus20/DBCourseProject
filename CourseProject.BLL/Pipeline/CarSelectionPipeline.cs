using CourseProject.BLL.FilterModels;
using CourseProject.BLL.PipelineBuilders;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.Pipeline {
    public class CarSelectionPipeline : Pipeline<SelectionPipelineExpressions<Car>, CarFilterModel> {

        private readonly CarFilterModel _filterModel;

        private readonly IPipelineBuilderDirector<Car, CarFilterModel> _builderDirector;

        public CarSelectionPipeline(CarFilterModel filterModel, IPipelineBuilderDirector<Car, CarFilterModel> builderDirector) {
            _filterModel = filterModel;
            _builderDirector = builderDirector;
        }

        public override SelectionPipelineExpressions<Car> Process() {

            var expressions = new SelectionPipelineExpressions<Car>();

            _builderDirector.Construct().AddExpression(expressions, _filterModel);

            return expressions;
        }
    }
}