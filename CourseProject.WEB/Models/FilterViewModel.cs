namespace CourseProject.WEB.Models; 

public abstract class FilterViewModel {

    public int PageNumber { get; set; } = 1;

    public int TakeCount { get; set; } = 10;
}