using CourseProject.BLL.DataHandlers;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.PipelineBuilders {
    public interface IPipelineBuilder<TExpressions, TFilterModel> {

        IPipelineBuilder<TExpressions, TFilterModel> SetFirstChainPart<T>() where T : IDataHandler<TExpressions, TFilterModel>;

        IPipelineBuilder<TExpressions, TFilterModel> SetNextChainPart<T>() where T : IDataHandler<TExpressions, TFilterModel>;

        IDataHandler<SelectionPipelineExpressions<Car>, TFilterModel> GetPipeline();
    }
}