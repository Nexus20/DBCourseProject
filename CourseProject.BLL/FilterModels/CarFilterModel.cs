namespace CourseProject.BLL.FilterModels; 

public class CarFilterModel : FilterModel {
    public uint? BrandId { get; set; }

    public uint? ModelId { get; set; }

    public CarOrderType? OrderType { get; set; }

    public string Model { get; set; }
}