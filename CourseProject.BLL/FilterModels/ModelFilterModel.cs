namespace CourseProject.BLL.FilterModels; 

public class ModelFilterModel : FilterModel {
    public uint? BrandId { get; set; }

    public string Model { get; set; }

    public ModelOrderType OrderType { get; set; }
}