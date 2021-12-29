using CourseProject.BLL.FilterModels;

namespace CourseProject.WEB.Models.FilterViewModels; 

public class BrandFilterViewModel : FilterViewModel {

    public string? Brand { get; set; }

    public BrandOrderType OrderType { get; set; }
}