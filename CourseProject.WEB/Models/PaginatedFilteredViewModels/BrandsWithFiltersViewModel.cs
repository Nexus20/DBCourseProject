using CourseProject.BLL.FilterModels;
using CourseProject.WEB.Models.FilterViewModels;

namespace CourseProject.WEB.Models.PaginatedFilteredViewModels; 

public class BrandsWithFiltersViewModel {

    public List<BrandViewModel> Brands { get; set; }

    public BrandFilterViewModel Filters { get; set; }

    public PageViewModel PageViewModel { get; set; }

    public BrandOrderType? SelectedOrderType { get; set; }

    public string Brand { get; set; } = "";
}