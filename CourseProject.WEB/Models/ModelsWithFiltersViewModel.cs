using CourseProject.BLL.FilterModels;

namespace CourseProject.WEB.Models; 

public class ModelsWithFiltersViewModel {

    public IEnumerable<ModelViewModel> Models { get; set; }

    public ModelFilterViewModel Filters { get; set; }

    public PageViewModel PageViewModel { get; set; }

    public uint? SelectedBrand { get; set; }

    public ModelOrderType? SelectedOrderType { get; set; }

    public string Model { get; set; } = "";
}