namespace CourseProject.BLL.Pipeline {
    public abstract class Pipeline<TResult, TModel> {

        public abstract TResult Process();
    }
}