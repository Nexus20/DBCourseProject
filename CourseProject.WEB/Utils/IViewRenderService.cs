namespace CourseProject.WEB.Utils; 

public interface IViewRenderService {
    string RenderToString(string viewName, object model);
}