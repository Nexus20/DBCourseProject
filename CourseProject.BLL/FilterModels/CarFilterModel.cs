namespace CourseProject.BLL.FilterModels; 

public class CarFilterModel {
    public uint BrandId { get; set; }

    public uint ModelId { get; set; }

    public CarOrderType OrderType { get; set; }

    public int? SkipCount { get; set; }

    public int? TakeCount { get; set; }
}