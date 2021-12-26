namespace CourseProject.BLL.FilterModels; 

public abstract class FilterModel {
    public int PageNumber { get; set; }

    public int TakeCount { get; set; }
}