using CourseProject.BLL.FilterModels;
using CourseProject.WEB.Models.FilterViewModels;

namespace CourseProject.WEB.Models.PaginatedFilteredViewModels; 

public class CarsInStockWithFiltersViewModel {

    public IEnumerable<CarInStockViewModel> Cars { get; set; }

    public CarInStockFilterViewModel Filters { get; set; }

    public PageViewModel PageViewModel { get; set; }

    public uint? SelectedBrand { get; set; }

    public uint? SelectedModel { get; set; }

    public uint? SelectedShowroom { get; set; }

    public CarOrderType? SelectedOrderType { get; set; }

    public string Model { get; set; } = "";
}