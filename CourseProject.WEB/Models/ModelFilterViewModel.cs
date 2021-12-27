using CourseProject.BLL.FilterModels;

namespace CourseProject.WEB.Models; 

public class ModelFilterViewModel : FilterViewModel {
    public uint? BrandId { get; set; }

    public string Model { get; set; }

    public ModelOrderType OrderType { get; set; }
}