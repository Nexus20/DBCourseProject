using CourseProject.BLL.FilterModels;
using CourseProject.WEB.Models.FilterViewModels;

namespace CourseProject.WEB.Models.PaginatedFilteredViewModels; 

public class ModelsWithFiltersViewModel {

    public IEnumerable<ModelViewModel> Models { get; set; }

    public ModelFilterViewModel Filters { get; set; }

    public PageViewModel PageViewModel { get; set; }

    public uint? SelectedBrand { get; set; }

    public ModelOrderType? SelectedOrderType { get; set; }

    public string Model { get; set; } = "";
}