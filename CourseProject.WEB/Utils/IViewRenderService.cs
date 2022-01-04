namespace CourseProject.WEB.Utils; 

public interface IViewRenderService {
    Task<string> RenderToString(string viewName, object model);
}