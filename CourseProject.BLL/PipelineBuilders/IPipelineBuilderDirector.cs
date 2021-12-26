using CourseProject.BLL.DataHandlers;

namespace CourseProject.BLL.PipelineBuilders {
    public interface IPipelineBuilderDirector<TExpressions, TFilterModel> {
        IDataHandler<TExpressions, TFilterModel> Construct();
    }
}