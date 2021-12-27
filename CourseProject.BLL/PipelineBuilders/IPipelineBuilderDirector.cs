using CourseProject.BLL.DataHandlers;

namespace CourseProject.BLL.PipelineBuilders {
    public interface IPipelineBuilderDirector<TEntity, TFilterModel> where TEntity : class {
        IDataHandler<TEntity, TFilterModel> Construct();
    }
}