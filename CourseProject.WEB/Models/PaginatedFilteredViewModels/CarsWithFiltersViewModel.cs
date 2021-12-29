using CourseProject.BLL.FilterModels;
using CourseProject.WEB.Models.FilterViewModels;

namespace CourseProject.WEB.Models.PaginatedFilteredViewModels; 

public class CarsWithFiltersViewModel {

    public IEnumerable<CarViewModel> Cars { get; set; }

    public CarFilterViewModel Filters { get; set; }

    public PageViewModel PageViewModel { get; set; }

    public uint? SelectedBrand { get; set; }

    public uint? SelectedModel { get; set; }

    public CarOrderType? SelectedOrderType { get; set; }

    public string Model { get; set; } = "";
}