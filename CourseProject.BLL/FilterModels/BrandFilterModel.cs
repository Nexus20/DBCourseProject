namespace CourseProject.BLL.FilterModels; 

public class BrandFilterModel : FilterModel {

    public string Brand { get; set; }

    public BrandOrderType OrderType { get; set; }
}